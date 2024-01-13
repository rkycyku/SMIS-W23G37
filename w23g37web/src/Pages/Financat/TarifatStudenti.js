/* eslint-disable no-undef */
import React, { useState, useEffect } from "react";
import "../Styles/ProductTables.css";
import Button from "react-bootstrap/Button";
import axios from "axios";
import Mesazhi from "../../Components/TeTjera/layout/Mesazhi";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faFileArrowDown,
  faInfoCircle,
  faPenToSquare,
  faReceipt,
  faScroll,
} from "@fortawesome/free-solid-svg-icons";
import DetajetStudenti from "../../Components/Administrata/DetajetStudenti";
import VertetimiStudentor from "../../Components/Administrata/VertetimiStudentor";
import { TailSpin } from "react-loader-spinner";
import { Helmet } from "react-helmet";
import { Form, Row, Col } from "react-bootstrap";
import TranskriptaNotave from "../../Components/Administrata/TranskriptaNotave/TranskriptaNotave";
import { Modal } from "react-bootstrap";
import PagesatStudentit from "../../Components/Studenti/PagesatStudentit/PagesatStudentit";
import { useNavigate } from "react-router-dom";

const TarifatStudenti = () => {
  const [teDhenat, setTeDhenat] = useState([]);
  const [id, setId] = useState();
  const [perditeso, setPerditeso] = useState("");
  const [shfaqPagesatStudentit, setShfaqPagesatStudentit] = useState(false);
  const [konfirmoVendosjenZbritjes, setKonfirmoVendosjenZbritjes] =
    useState(false);
  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");
  const [loading, setLoading] = useState(false);
  const [kerkimi, setKerkimi] = useState("");

  const getToken = localStorage.getItem("token");
  const getID = localStorage.getItem("id");
  const navigate = useNavigate();

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const shfaqStudentet = async () => {
      try {
        setLoading(true);

        const rolet = await axios.get(
          `https://localhost:7251/api/Perdoruesi/shfaqSipasID?idUserAspNet=${getID}`,
          authentikimi
        );
        if (!rolet.data.rolet.includes("Financa")) {
          navigate("/NukKeniAkses");
        }

        const teDhenat = await axios.get(
          "https://localhost:7251/api/Financat/ShfaqStudentet",
          authentikimi
        );
        setTeDhenat(teDhenat.data);
        setLoading(false);
      } catch (err) {
        console.log(err);
        setLoading(false);
      }
    };

    shfaqStudentet();
  }, [perditeso]);

  async function VendosZbritjenStudentit() {
    await axios.put(
      `https://localhost:7251/api/Financat/PerditesoniTarifenStudentit?StudentiID=${id}`,
      authentikimi
    );

    setKonfirmoVendosjenZbritjes(false);
    setPerditeso(Date.now());
  }

  const handleKonfirmoVendosjenZbritjes = (id) => {
    setKonfirmoVendosjenZbritjes(true);
    setId(id);
  };
  const handleShfaqPagesatStudentit = (id) => {
    setShfaqPagesatStudentit(true);
    setId(id);
  };

  const handleKerkimi = (event) => {
    setKerkimi(event.target.value);
  };

  const filtrimiTeDhenave = teDhenat.filter((student) => {
    return (
      student.studenti.studenti.toLowerCase().includes(kerkimi.toLowerCase()) ||
      student.studenti.idStudentiFK
        .toLowerCase()
        .includes(kerkimi.toLowerCase()) ||
      student.studenti.kodiFinanciar
        .toLowerCase()
        .includes(kerkimi.toLowerCase()) ||
      student.teDhenatRegjistrimit.departamenti
        .toLowerCase()
        .includes(kerkimi.toLowerCase()) ||
      student.teDhenatRegjistrimit.shkurtesaDepartamentit
        .toLowerCase()
        .includes(kerkimi.toLowerCase()) ||
      student.teDhenatRegjistrimit.niveliStudimeve
        .toLowerCase()
        .includes(kerkimi.toLowerCase()) ||
      student.teDhenatRegjistrimit.shkurtesaNivelitStudimeve
        .toLowerCase()
        .includes(kerkimi.toLowerCase())
    );
  });

  return (
    <div className="containerDashboardP">
      <Helmet>
        <title>Lista Studenteve | UBT SMIS</title>
      </Helmet>
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
          {konfirmoVendosjenZbritjes && (
            <Modal show="true">
              <Modal.Header>
                <Modal.Title style={{ color: "#009879" }}>
                  Konfirmimi
                </Modal.Title>
              </Modal.Header>
              <Modal.Body>
                A jeni te sigurt qe deshironi te shtoni zbritjen vjetore per
                kete student?
              </Modal.Body>
              <Modal.Footer>
                <Button
                  onClick={() => setKonfirmoVendosjenZbritjes(false)}
                  variant={"outline-danger"}>
                  JO
                </Button>
                <Button
                  onClick={() => VendosZbritjenStudentit()}
                  variant={"outline-success"}>
                  PO
                </Button>
              </Modal.Footer>
            </Modal>
          )}
          {shfaqPagesatStudentit && (
            <PagesatStudentit
              setMbyllTeDhenat={() => {
                setShfaqPagesatStudentit(false);
                setPerditeso(Date.now());
              }}
              id={id}
            />
          )}
          <>
            <Row className="mb-3 gap-1">
              <Col xs={6} md={4} className="p-0">
                <Form.Group controlId="search">
                  <Form.Control
                    type="text"
                    placeholder="Kerkoni Studentin"
                    value={kerkimi}
                    onChange={handleKerkimi}
                  />
                </Form.Group>
              </Col>
            </Row>
            <table className="tableBig">
              <thead>
                <tr style={{ fontSize: "12px" }}>
                  <th>ID Studenti</th>
                  <th>Studenti</th>
                  <th>Fakulteti</th>
                  <th>Kodi Financiar</th>
                  <th>Pagesat</th>
                  <th>Faturimi</th>
                  <th>Mbetja</th>
                  <th>Kesti Vjetor</th>
                  <th>Kesti Mujor</th>
                  <th>Zbrijta 1</th>
                  <th>Zbrijta 2</th>
                  <th></th>
                </tr>
              </thead>

              <tbody>
                {filtrimiTeDhenave &&
                  filtrimiTeDhenave.map((p) => {
                    return (
                      <tr
                        style={{ fontSize: "12px" }}
                        key={p.studenti && p.studenti.aspNetUserID}>
                        <td>{p.studenti && p.studenti.idStudentiFK}</td>
                        <td>{p.studenti && p.studenti.studenti}</td>
                        <td>
                          {p.teDhenatRegjistrimit &&
                            p.teDhenatRegjistrimit.shkurtesaDepartamentit}{" "}
                          -{" "}
                          {p.teDhenatRegjistrimit &&
                            p.teDhenatRegjistrimit.shkurtesaNivelitStudimeve}
                        </td>
                        <td>{p.studenti && p.studenti.kodiFinanciar}</td>
                        <td>
                          {p.pagesatDheTarifa &&
                            parseFloat(p.pagesatDheTarifa.totPagesat).toFixed(
                              2
                            )}{" "}
                          €
                        </td>
                        <td>
                          {p.pagesatDheTarifa &&
                            parseFloat(p.pagesatDheTarifa.totTarifat).toFixed(
                              2
                            )}{" "}
                          €
                        </td>
                        <td>
                          {p.pagesatDheTarifa &&
                            parseFloat(
                              p.pagesatDheTarifa.totPagesat -
                                p.pagesatDheTarifa.totTarifat
                            ).toFixed(2)}{" "}
                          €
                        </td>
                        <td>
                          {p.pagesatDheTarifa &&
                            parseFloat(
                              p.pagesatDheTarifa.tarifaDepartamentit
                            ).toFixed(2)}{" "}
                          €
                        </td>
                        <td>
                          {p.pagesatDheTarifa &&
                            parseFloat(
                              p.pagesatDheTarifa.tarifaDepartamentit / 12
                            ).toFixed(2)}{" "}
                          €
                        </td>
                        <td>
                          {p.pagesatDheTarifa &&
                            p.pagesatDheTarifa.zbritja1 &&
                            p.pagesatDheTarifa.zbritja1.emriZbritjes}{" "}
                          -{" "}
                          {p.pagesatDheTarifa &&
                            p.pagesatDheTarifa.zbritja1 &&
                            p.pagesatDheTarifa.zbritja1.zbritja}
                          %
                        </td>
                        <td>
                          {p.pagesatDheTarifa &&
                            p.pagesatDheTarifa.zbritja2 &&
                            p.pagesatDheTarifa.zbritja2.emriZbritjes}{" "}
                          -{" "}
                          {p.pagesatDheTarifa &&
                            p.pagesatDheTarifa.zbritja2 &&
                            p.pagesatDheTarifa.zbritja2.zbritja}
                          %
                        </td>
                        <td>
                          <Button
                            style={{
                              marginRight: "0.2em",
                              marginBottom: "0.2em",
                            }}
                            variant="success"
                            onClick={() =>
                              handleShfaqPagesatStudentit(
                                p.studenti && p.studenti.aspNetUserID
                              )
                            }>
                            <FontAwesomeIcon icon={faInfoCircle} />
                          </Button>
                          <Button
                            style={{
                              marginRight: "0.2em",
                              marginBottom: "0.2em",
                            }}
                            variant="success"
                            onClick={() =>
                              handleKonfirmoVendosjenZbritjes(
                                p.studenti && p.studenti.aspNetUserID
                              )
                            }>
                            <FontAwesomeIcon icon={faPenToSquare} />
                          </Button>
                        </td>
                      </tr>
                    );
                  })}
              </tbody>
            </table>
          </>
        </>
      )}
    </div>
  );
};

export default TarifatStudenti;
