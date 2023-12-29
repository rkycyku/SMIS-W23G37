import { useEffect, useState } from "react";
import "./../../../Pages/Styles/DizajniPergjithshem.css";
import axios from "axios";
import Button from "react-bootstrap/Button";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faPlus,
  faFileInvoice,
  faXmark,
  faPenToSquare,
} from "@fortawesome/free-solid-svg-icons";
import { TailSpin } from "react-loader-spinner";
import { Table, Container, Row, Col } from "react-bootstrap";
import Mesazhi from "../../TeTjera/layout/Mesazhi";
import EditoTarifen from "./EditoTarifen";

function TeDhenatKalkulimit(props) {
  const [perditeso, setPerditeso] = useState("");
  const [loading, setLoading] = useState(false);
  
  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");

  const [teDhenat, setTeDhenat] = useState([]);
  const [edito, setEdito] = useState(false);
  const [show, setShow] = useState(false);
  const [id, setId] = useState();

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const vendosTeDhenat = async () => {
      try {
        setLoading(true);
        const tarifat = await axios.get(
          `https://localhost:7251/api/Financat/ShfaqniTarifatPerDepartamentin?DepartamentiID=${props.id}`,
          authentikimi
        );
        setTeDhenat(tarifat.data);
        setLoading(false);
      } catch (err) {
        console.log(err);
        setLoading(false);
      }
    };

    vendosTeDhenat();
  }, [perditeso]);

  const handleSave = () => {
    props.setMbyllTeDhenat();
  };
  
  const handleShow = () => setShow(true);

  const handleEdito = (id) => {
    setEdito(true);
    setId(id);
  };

  const handleEditoMbyll = () => setEdito(false);

  return (
    <>
    {shfaqMesazhin && (
      <Mesazhi
        setShfaqMesazhin={setShfaqMesazhin}
        pershkrimi={pershkrimiMesazhit}
        tipi={tipiMesazhit}
      />
    )}
    {edito && (
      <EditoTarifen
        show={handleShow}
        hide={handleEditoMbyll}
        id={id}
        shfaqmesazhin={() => setShfaqMesazhin(true)}
        perditesoTeDhenat={() => setPerditeso(Date.now())}
        setTipiMesazhit={setTipiMesazhit}
        setPershkrimiMesazhit={setPershkrimiMesazhit}
      />
    )}
      <div className="containerDashboardP">
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
            <Container fluid>
              <Row>
                <h1 className="title">Tarifat e Departamentit</h1>
              </Row>
              <Row className="mt-3">
                <Col className="mobileResponsive">
                  <h5>
                    <strong>Emri Departamentit:</strong>{" "}
                    {teDhenat &&
                      teDhenat.departamenti &&
                      teDhenat.departamenti.emriDepartamentit}
                  </h5>
                  <h5>
                    <strong>Shkurtesa Departamentit:</strong>{" "}
                    {teDhenat &&
                      teDhenat.departamenti &&
                      teDhenat.departamenti.shkurtesaDepartamentit}
                  </h5>
                </Col>

                <Button className="mb-3 Butoni" onClick={handleSave}>
                  Mbyll Te Dhenat <FontAwesomeIcon icon={faXmark} />
                </Button>
              </Row>
              <Table striped bordered hover>
                <thead>
                  <tr>
                    <th>Nr. Rendore</th>
                    <th>Niveli Studimeve</th>
                    <th>Taria Vjetore</th>
                    <th>Taria Mujore</th>
                    <th>Funksione</th>
                  </tr>
                </thead>
                <tbody>
                  {teDhenat &&
                    teDhenat.departamenti &&
                    teDhenat.departamenti.tarifatDepartamenti &&
                    teDhenat.departamenti.tarifatDepartamenti.map(
                      (tarifa, index) => (
                        <tr key={index}>
                          <td>{index + 1}</td>
                          <td>
                            {tarifa.niveliStudimeve &&
                              tarifa.niveliStudimeve.emriNivelitStudimeve}{" "}
                            -{" "}
                            {tarifa.niveliStudimeve &&
                              tarifa.niveliStudimeve
                                .shkurtesaEmritNivelitStudimeve}
                          </td>
                          <td>
                            {parseFloat(tarifa.tarifaVjetore).toFixed(2)} €
                          </td>
                          <td>
                            {parseFloat(tarifa.tarifaVjetore / 12).toFixed(2)} €
                          </td>
                          <td>
                            <Button
                              style={{ marginRight: "0.5em" }}
                              variant="success"
                              onClick={() =>
                                handleEdito(tarifa.tarifaID)
                              }>
                              <FontAwesomeIcon icon={faPenToSquare} />
                            </Button>
                          </td>
                        </tr>
                      )
                    )}
                </tbody>
              </Table>
            </Container>
          </>
        )}
      </div>
    </>
  );
}

export default TeDhenatKalkulimit;
