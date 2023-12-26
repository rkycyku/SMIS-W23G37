import { useEffect, useState } from "react";
import "../Styles/DizajniPergjithshem.css";
import axios from "axios";
import Button from "react-bootstrap/Button";
import Mesazhi from "../../Components/TeTjera/layout/Mesazhi";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faPlus,
  faXmark,
  faPenToSquare,
  faL,
  faArrowRotateForward,
} from "@fortawesome/free-solid-svg-icons";
import { TailSpin } from "react-loader-spinner";
import { Table, Form, Container, Row, Col } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { Modal } from "react-bootstrap";
import { MDBTable, MDBTableBody, MDBTableHead } from "mdb-react-ui-kit";
import { faCircleInfo } from "@fortawesome/free-solid-svg-icons";
import { Helmet } from "react-helmet";
import useKeyboardNavigation from "../../Context/useKeyboardNavigation";
import { DemoContainer } from "@mui/x-date-pickers/internals/demo";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DateField } from "@mui/x-date-pickers/DateField";
import Box from "@mui/material/Box";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import dayjs from "dayjs";

function RegjistrimiISemestrit(props) {
  const [perditeso, setPerditeso] = useState("");
  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");
  const [loading, setLoading] = useState(false);

  const [lokacioni, setLokacioni] = useState("");
  const [semestri, setSemestri] = useState("");
  const [orariMesimit, setOrariMesimit] = useState("Paradite");

  const [semestratEParaqitur, setSemestratEParaqitur] = useState([]);

  const [teDhenat, setTeDhenat] = useState([]);

  const navigate = useNavigate();

  const getID = localStorage.getItem("id");

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const shfaqSemestratEParaqitur = async () => {
      try {
        setLoading(true);
        const semestrat = await axios.get(
          `https://localhost:7251/api/Studentet/ShfaqSemestratERegjistruar?studentiID=${getID}`,
          authentikimi
        );
        
        setSemestratEParaqitur(semestrat.data);
        setLoading(false);
      } catch (err) {
        console.log(err);
        setLoading(false);
      }
    };

    shfaqSemestratEParaqitur();
  }, [perditeso]);

  useEffect(() => {
    if (getID) {
      const vendosTeDhenat = async () => {
        try {
          const perdoruesi = await axios.get(
            `https://localhost:7251/api/Studentet/ShfaqAfatinERegjistrimitSemestrit?studentID=${getID}`,
            authentikimi
          );
          setTeDhenat(perdoruesi.data);
        } catch (err) {
          console.log(err);
        } finally {
          setLoading(false);
        }
      };

      vendosTeDhenat();
    } else {
      navigate("/login");
    }
  }, [perditeso]);

  const ndrroField = (e, tjetra) => {
    if (e.key === "Enter") {
      e.preventDefault();
      document.getElementById(tjetra).focus();
    }
  };

  async function handleRegjistroSemestrin() {
    try {
      await axios
      .post(
        "https://localhost:7251/api/Studentet/RegjistroSemestrin",
        {
          "paraqitjaSemestrit": {
            "apsid": teDhenat && teDhenat.aps && teDhenat.aps.apsid,
            "semestriID": semestri,
            "studentiID": teDhenat && teDhenat.perdoruesi && teDhenat.perdoruesi.userID,
            "nderrimiOrarit": orariMesimit,
            "lokacioniID": lokacioni,
        }
        },
        authentikimi
      )
        .then((response) => {
          if (response.status === 200 || response.status === 201) {
            setPerditeso(Date.now());
          } else {
            console.log("gabim");
            setPerditeso(Date.now());
          }
        });
    } catch (error) {
      console.error(error);
    }
  }

  return (
    <>
      <Helmet>
        <title>Regjistrimi i semestrit | UBT SMIS</title>
      </Helmet>

      <div className="containerDashboardP" style={{ width: "90%" }}>
        {shfaqMesazhin && (
          <Mesazhi
            setShfaqMesazhin={setShfaqMesazhin}
            pershkrimi={pershkrimiMesazhit}
            tipi={tipiMesazhit}
          />
        )}
        {loading ? (
          <div className="Loader">
            <TailSpin
              height="80"
              width="80"
              color="#009879"
              ariaLabel="tail-spin-loading"
              radius="1"
              wrapperStyle={{}}
              wrapperClass=""
              visible={true}
            />
          </div>
        ) : (
            <>
              <h1 className="title">Regjistrimi i semestrit</h1>

              <Container fluid>
                <Row>
                  <Col>
                    <Form.Group>
                      <Form.Label>Lokacioni</Form.Label>
                      <select
                        id="llojiIPageses"
                        placeholder="LlojiIPageses"
                        className="form-select"
                        value={lokacioni ? lokacioni : 0}
                        onChange={(e) => {
                          setLokacioni(e.target.value);
                        }}
                        onKeyDown={(e) => {
                          ndrroField(e, "statusiIPageses");
                        }}>
                        <option defaultValue value={0} key={0} disabled>
                          Zgjedhni Lokacionin
                        </option>
                        {teDhenat &&
                          teDhenat.lokacionet &&
                          teDhenat.lokacionet.map((item) => (
                            <option
                              key={item.lokacioniID}
                              value={item.lokacioniID}>
                              {item.emriLokacionit}
                            </option>
                          ))}
                      </select>
                    </Form.Group>
                    <Form.Group>
                      <Form.Label>Semestri i studimeve</Form.Label>
                      <select
                        id="llojiIPageses"
                        placeholder="LlojiIPageses"
                        className="form-select"
                        value={semestri ? semestri : 0}
                        onChange={(e) => {
                          setSemestri(e.target.value);
                        }}
                        onKeyDown={(e) => {
                          ndrroField(e, "statusiIPageses");
                        }}>
                        <option defaultValue value={0} key={0} disabled>
                          Zgjedhni Semestrin
                        </option>
                        {teDhenat &&
                          teDhenat.semestrat &&
                          teDhenat.semestrat.map((item) => (
                            <option
                              key={item.semestriID}
                              value={item.semestriID}>
                              Semestri {item.nrSemestrit}
                            </option>
                          ))}
                      </select>
                    </Form.Group>
                    <Form.Group>
                      <Form.Label>Orari i mësimit</Form.Label>
                      <select
                        id="llojiIPageses"
                        placeholder="LlojiIPageses"
                        className="form-select"
                        value={orariMesimit}
                        onChange={(e) => {
                          setOrariMesimit(e.target.value);
                        }}
                        onKeyDown={(e) => {
                          ndrroField(e, "statusiIPageses");
                        }}>
                        <option defaultValue value={0} key={0} disabled>
                          Zgjedhni Orari e mësimit
                        </option>
                        <option key={1} value="Paradite">
                        Paradite
                        </option>
                        <option key={2} value="Pasdite">
                        Pasdite
                        </option>
                      </select>
                    </Form.Group>
                  </Col>
                  <Col>
                    <br />
                    <Button
                      className="mb-3 Butoni"
                      onClick={() => handleRegjistroSemestrin()}>
                      Regjistro Semestrin
                      <FontAwesomeIcon icon={faPlus} />
                    </Button>
                    <br />
                    <Button
                      className="mb-3 Butoni"
                      onClick={() => handleRegjistroSemestrin()}>
                      Çregjistro Semestrin
                      <FontAwesomeIcon icon={faPlus} />
                    </Button>
                  </Col>
                </Row>
                <h1 className="title">Lista e regjistrimeve të semestrave</h1>

                <MDBTable style={{ width: "100%" }}>
                  <MDBTableHead>
                    <tr>
                      <th scope="col">Afati</th>
                      <th scope="col">Lokacioni</th>
                      <th scope="col">Semestri</th>
                      <th scope="col">Ndërrimi i orarit të mësimit</th>
                      <th scope="col">Data e regjistrimit</th>
                    </tr>
                  </MDBTableHead>

                  <MDBTableBody>
                    {semestratEParaqitur
                      .map((k) => (
                        <tr key={k.paraqitjaSemestritID}>
                          <td>Regjistrimi i semestrit {k.afatiParaqitjesSemestrit && k.afatiParaqitjesSemestrit.llojiSemestrit} {k.afatiParaqitjesSemestrit && k.afatiParaqitjesSemestrit.vitiAkademik} - {k.afatiParaqitjesSemestrit && k.afatiParaqitjesSemestrit.niveliStudimeve && k.afatiParaqitjesSemestrit.niveliStudimeve.shkurtesaEmritNivelitStudimeve}</td>
                          <td>{k.lokacioni && k.lokacioni.emriLokacionit}</td>
                          <td>Semestri {k.semestri && k.semestri.nrSemestrit}</td>
                          <td>
                          {k.nderrimiOrarit}
                          </td>
                          <td>{new Date(k.dataParaqitjes).toLocaleString("en-GB", { dateStyle: "short", timeStyle: "medium" })}</td>
                        </tr>
                      ))}
                  </MDBTableBody>
                </MDBTable>
              </Container>
            </>
          )
        }
      </div>
    </>
  );
}

export default RegjistrimiISemestrit;
