/* eslint-disable no-undef */
import React, { useState, useEffect } from "react";
import "../Styles/ProductTables.css";
import Button from "react-bootstrap/Button";
import axios from "axios";
import Mesazhi from "../../Components/TeTjera/layout/Mesazhi";
import ShtoPagesen from "../../Components/Financat/Pagesat/ShtoPagesen";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faBan,
  faPenToSquare,
  faPlus,
  faXmark,
  faCheck,
  faInfoCircle,
  faArrowRotateForward,
} from "@fortawesome/free-solid-svg-icons";
import Modal from "react-bootstrap/Modal";
import { TailSpin } from "react-loader-spinner";
import { Helmet } from "react-helmet";
import { DemoContainer } from "@mui/x-date-pickers/internals/demo";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DateField } from "@mui/x-date-pickers/DateField";
import Box from "@mui/material/Box";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import { useNavigate } from "react-router-dom";

const Pagesat = () => {
  const [pagesat, setPagesat] = useState([]);
  const [id, setId] = useState();
  const [perditeso, setPerditeso] = useState("");
  const [show, setShow] = useState(false);
  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");
  const [loading, setLoading] = useState(false);

  const [dataFillestare, setDataFillestare] = useState(null);
  const [dataFundit, setDataFundit] = useState(null);
  const [filtroStatusi, setFiltroStatusi] = useState("Te Gjitha");

  const getToken = localStorage.getItem("token");
  const getID = localStorage.getItem("id");
  const navigate = useNavigate();

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const shfaqPagesat = async () => {
      try {
        setLoading(true);

        const rolet = await axios.get(
          `https://localhost:7251/api/Perdoruesi/shfaqSipasID?idUserAspNet=${getID}`,
          authentikimi
        );
        if (!rolet.data.rolet.includes("Financa")) {
          navigate("/NukKeniAkses");
        }

        const pagesat = await axios.get(
          "https://localhost:7251/api/Financat/ShfaqniPagesat",
          authentikimi
        );
        setPagesat(pagesat.data);
        setLoading(false);
      } catch (err) {
        console.log(err);
        setLoading(false);
      }
    };

    shfaqPagesat();
  }, [perditeso]);

  const handleClose = () => {
    setShow(false);
  };
  const handleShow = () => setShow(true);

  const handleEdito = (id) => {
    setEdito(true);
    setId(id);
  };

  const [showD, setShowD] = useState(false);

  const handleCloseD = () => setShowD(false);
  const handleShowD = (id) => {
    setId(id);
    setShowD(true);
  };

  const handleEditoMbyll = () => setEdito(false);

  async function handleDelete() {
    try {
      await axios.delete(
        `https://localhost:7251/api/Financat/FshiniPagesen?pagesaID=` + id,
        authentikimi
      );
      setTipiMesazhit("success");
      setPershkrimiMesazhit("Pagesa u fshi me sukses!");
      setPerditeso(Date.now());
      setShfaqMesazhin(true);
      setShowD(false);
    } catch (err) {
      console.error(err);
      setTipiMesazhit("danger");
      setPershkrimiMesazhit("Ndodhi nje gabim gjate fshirjes se Pageses!");
      setPerditeso(Date.now());
      setShfaqMesazhin(true);
      setShowD(false);
    }
  }

  return (
    <div className="containerDashboardP">
      <Helmet>
        <title>Pagesat Hyrese / Dalese | UBT SMIS</title>
      </Helmet>
      {show && (
        <ShtoPagesen
          show={handleShow}
          hide={handleClose}
          shfaqmesazhin={() => setShfaqMesazhin(true)}
          perditesoTeDhenat={() => setPerditeso(Date.now())}
          setTipiMesazhit={setTipiMesazhit}
          setPershkrimiMesazhit={setPershkrimiMesazhit}
        />
      )}
      {shfaqMesazhin && (
        <Mesazhi
          setShfaqMesazhin={setShfaqMesazhin}
          pershkrimi={pershkrimiMesazhit}
          tipi={tipiMesazhit}
        />
      )}
      <Modal show={showD} onHide={handleCloseD}>
        <Modal.Header closeButton>
          <Modal.Title style={{ color: "red" }}>Fshini Pagesen</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <h6>A jeni te sigurt qe deshironi ta fshini kete Pagese?</h6>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseD}>
            Anulo <FontAwesomeIcon icon={faXmark} />
          </Button>
          <Button variant="danger" onClick={handleDelete}>
            Fshini Pagesen <FontAwesomeIcon icon={faBan} />
          </Button>
        </Modal.Footer>
      </Modal>
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
          <Button className="mb-3 Butoni" onClick={handleShow}>
            Shto Pagesen <FontAwesomeIcon icon={faPlus} />
          </Button>
          <div className="DataPerFiltrim">
            <div>
              <LocalizationProvider dateAdapter={AdapterDayjs}>
                <DemoContainer components={["DateField", "DateField"]}>
                  <DateField
                    label="Data Fillestare"
                    value={dataFillestare}
                    onChange={(date) => setDataFillestare(date)}
                    size="small"
                    format="DD/MM/YYYY"
                  />
                </DemoContainer>
              </LocalizationProvider>
            </div>
            <div>
              <LocalizationProvider dateAdapter={AdapterDayjs}>
                <DemoContainer components={["DateField", "DateField"]}>
                  <DateField
                    label="Data Fundit"
                    value={dataFundit}
                    onChange={(date) => setDataFundit(date)}
                    size="small"
                    format="DD/MM/YYYY"
                  />
                </DemoContainer>
              </LocalizationProvider>
            </div>
            <div>
              <Box sx={{ minWidth: 120 }}>
                <FormControl fullWidth size="small">
                  <InputLabel id="demo-simple-select-label">
                    Statusi i Fatures
                  </InputLabel>
                  <Select
                    labelId="demo-simple-select-label"
                    id="demo-simple-select"
                    value={filtroStatusi}
                    label="Statusi i Fatures"
                    onChange={(e) => {
                      setFiltroStatusi(e.target.value);
                    }}>
                    <MenuItem defaultValue value="Te Gjitha" key="Te Gjitha">
                      Te Gjitha
                    </MenuItem>
                    <MenuItem key="Hyrese" value="Hyrese">
                      Hyrese
                    </MenuItem>
                    <MenuItem key="Dalese" value="Dalese">
                      Dalese
                    </MenuItem>
                  </Select>
                </FormControl>
              </Box>
            </div>
            <div className="datat">
              <Button
                style={{ marginRight: "0.5em" }}
                variant="success"
                onClick={() => {
                  setDataFillestare(null);
                  setDataFundit(null);
                  setFiltroStatusi("Te Gjitha");
                }}>
                Reseto <FontAwesomeIcon icon={faArrowRotateForward} />
              </Button>
            </div>
          </div>
          <table className="tableBig">
            <thead>
              <tr>
                <th>Lloji Pageses</th>
                <th>Banka</th>
                <th>E bere nga/per</th>
                <th>Shuma</th>
                <th>Pershkrimi Pageses</th>
                <th>Data Pageses</th>
                <th>Funksione</th>
              </tr>
            </thead>

            <tbody>
              {pagesat
                .filter((p) => {
                  if (!dataFillestare || !dataFundit) {
                    return true;
                  } else {
                    const dataPageses = new Date(p.dataPageses);
                    return (
                      dataPageses >= dataFillestare && dataPageses <= dataFundit
                    );
                  }
                })
                .filter((p) => {
                  if (filtroStatusi === "Te Gjitha") {
                    return true;
                  } else {
                    return p.llojiPageses === filtroStatusi;
                  }
                })
                .filter((p) => {
                  if (p.llojiPageses != "TarifaStudentit") {
                    return true;
                  }
                })
                .map((p) => {
                  return (
                    <tr key={p.pagesaID}>
                      <td>{p.llojiPageses}</td>
                      <td>
                        {p.bankat && p.bankat.kodiBankes} -{" "}
                        {p.bankat && p.bankat.emriBankes}
                      </td>
                      <td>
                        {p.aplikimiID != null
                          ? (p.aplikimetEReja &&
                              p.aplikimetEReja.kodiFinanciar) +
                            " - " +
                            p.aplikimetEReja.emri +
                            " " +
                            p.aplikimetEReja.mbiemri
                          : p.perdoruesi &&
                            p.perdoruesi.teDhenatRegjistrimitStudentit &&
                            p.perdoruesi.teDhenatRegjistrimitStudentit
                              .kodiFinanciar != null
                          ? (p.perdoruesi &&
                              p.perdoruesi.teDhenatRegjistrimitStudentit &&
                              p.perdoruesi.teDhenatRegjistrimitStudentit
                                .kodiFinanciar) +
                            " - " +
                            (p.perdoruesi && p.perdoruesi.emri) +
                            " " +
                            (p.perdoruesi && p.perdoruesi.mbiemri)
                          : (p.perdoruesi && p.perdoruesi.username) +
                            " - " +
                            (p.perdoruesi && p.perdoruesi.emri) +
                            " " +
                            (p.perdoruesi && p.perdoruesi.mbiemri)}
                      </td>
                      <td>
                        {p.pagesa != null
                          ? parseFloat(p.pagesa).toFixed(2)
                          : parseFloat(p.faturimi).toFixed(2)}{" "}
                        €
                      </td>
                      <td>{p.pershkrimiPageses}</td>
                      <td>
                        {new Date(p.dataPageses).toLocaleDateString("en-GB", {
                          dateStyle: "short",
                        })}
                      </td>

                      <td>
                        <Button
                          variant="danger"
                          onClick={() => handleShowD(p.pagesaID)}>
                          <FontAwesomeIcon icon={faBan} />
                        </Button>
                      </td>
                    </tr>
                  );
                })}
            </tbody>
          </table>
        </>
      )}
    </div>
  );
};

export default Pagesat;
