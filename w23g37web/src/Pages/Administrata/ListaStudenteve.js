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
import PagesatStudentit from "../../Components/Studenti/PagesatStudentit/PagesatStudentit";
import { useNavigate } from "react-router-dom";

const ListaStudenteve = () => {
  const [teDhenat, setTeDhenat] = useState([]);
  const [id, setId] = useState();
  const [perditeso, setPerditeso] = useState("");
  const [shfaqTeDhenat, setShfaqTeDhenat] = useState(false);
  const [shfaqVertetiminStudentor, setShfaqVertetiminStudentore] =
    useState(false);
  const [shfaqTranskriptenENotave, setShfaqTranskriptenENotave] =
    useState(false);
  const [shfaqPagesatStudentit, setShfaqPagesatStudentit] = useState(false);
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
        if (!rolet.data.rolet.includes("Administrat")) {
          navigate("/NukKeniAkses");
        }

        const teDhenat = await axios.get(
          "https://localhost:7251/api/Administrata/ShfaqStudentet",
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

  const handleShfaqTeDhenat = (id) => {
    setShfaqTeDhenat(true);
    setShfaqVertetiminStudentore(false);
    setShfaqTranskriptenENotave(false);
    setShfaqPagesatStudentit(false);
    setId(id);
  };

  const handleShfaqVertetiminStudentore = (id) => {
    setShfaqTeDhenat(false);
    setShfaqVertetiminStudentore(true);
    setShfaqTranskriptenENotave(false);
    setShfaqPagesatStudentit(false);
    setId(id);
  };

  const handleShfaqTranskriptenENotave = (id) => {
    setShfaqTeDhenat(false);
    setShfaqVertetiminStudentore(false);
    setShfaqTranskriptenENotave(true);
    setShfaqPagesatStudentit(false);
    setId(id);
  };

  const handleShfaqPagesatStudentit = (id) => {
    setShfaqTeDhenat(false);
    setShfaqVertetiminStudentore(false);
    setShfaqTranskriptenENotave(false);
    setShfaqPagesatStudentit(true);
    setId(id);
  };

  const handleKerkimi = (event) => {
    setKerkimi(event.target.value);
  };

  const filtrimiTeDhenave = teDhenat.filter((student) => {
    const emriPlote = `${student.emri} ${
      student.teDhenatPerdoruesit ? student.teDhenatPerdoruesit.emriPrindit : ""
    } ${student.mbiemri}`.toLowerCase();

    return (
      emriPlote.includes(kerkimi.toLowerCase()) ||
      (student.teDhenatRegjistrimitStudentit &&
        student.teDhenatRegjistrimitStudentit.idStudenti
          .toLowerCase()
          .includes(kerkimi.toLowerCase())) ||
      (student.teDhenatRegjistrimitStudentit &&
        student.teDhenatRegjistrimitStudentit.kodiFinanciar
          .toLowerCase()
          .includes(kerkimi.toLowerCase())) ||
      (student.teDhenatRegjistrimitStudentit &&
        student.teDhenatRegjistrimitStudentit.vitiAkademikRegjistrim
          .toLowerCase()
          .includes(kerkimi.toLowerCase())) ||
      (student.teDhenatRegjistrimitStudentit &&
        student.teDhenatRegjistrimitStudentit.niveliStudimeve &&
        student.teDhenatRegjistrimitStudentit.niveliStudimeve.shkurtesaEmritNivelitStudimeve
          .toLowerCase()
          .includes(kerkimi.toLowerCase())) ||
      (student.teDhenatRegjistrimitStudentit &&
        student.teDhenatRegjistrimitStudentit.niveliStudimeve &&
        student.teDhenatRegjistrimitStudentit.niveliStudimeve.emriNivelitStudimeve
          .toLowerCase()
          .includes(kerkimi.toLowerCase())) ||
      (student.teDhenatRegjistrimitStudentit &&
        student.teDhenatRegjistrimitStudentit.departamentet &&
        student.teDhenatRegjistrimitStudentit.departamentet.shkurtesaDepartamentit
          .toLowerCase()
          .includes(kerkimi.toLowerCase())) ||
      (student.teDhenatRegjistrimitStudentit &&
        student.teDhenatRegjistrimitStudentit.departamentet &&
        student.teDhenatRegjistrimitStudentit.departamentet.emriDepartamentit
          .toLowerCase()
          .includes(kerkimi.toLowerCase()))
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
          {shfaqTeDhenat &&
            !shfaqVertetiminStudentor &&
            !shfaqTranskriptenENotave &&
            !shfaqPagesatStudentit && (
              <DetajetStudenti
                setMbyllTeDhenat={() => {
                  setShfaqTeDhenat(false);
                  setShfaqVertetiminStudentore(false);
                  setShfaqTranskriptenENotave(false);
                  setShfaqPagesatStudentit(false);
                  setPerditeso(Date.now());
                }}
                id={id}
              />
            )}
          {shfaqVertetiminStudentor &&
            !shfaqTeDhenat &&
            !shfaqTranskriptenENotave &&
            !shfaqPagesatStudentit && (
              <VertetimiStudentor
                setMbyllTeDhenat={() => {
                  setShfaqTeDhenat(false);
                  setShfaqVertetiminStudentore(false);
                  setShfaqTranskriptenENotave(false);
                  setShfaqPagesatStudentit(false);
                  setPerditeso(Date.now());
                }}
                id={id}
              />
            )}
          {shfaqTranskriptenENotave &&
            !shfaqVertetiminStudentor &&
            !shfaqTeDhenat &&
            !shfaqPagesatStudentit && (
              <TranskriptaNotave
                setMbyllTeDhenat={() => {
                  setShfaqTeDhenat(false);
                  setShfaqVertetiminStudentore(false);
                  setShfaqTranskriptenENotave(false);
                  setShfaqPagesatStudentit(false);
                  setPerditeso(Date.now());
                }}
                id={id}
              />
            )}
          {shfaqPagesatStudentit &&
            !shfaqTranskriptenENotave &&
            !shfaqVertetiminStudentor &&
            !shfaqTeDhenat && (
              <PagesatStudentit
                setMbyllTeDhenat={() => {
                  setShfaqTeDhenat(false);
                  setShfaqVertetiminStudentore(false);
                  setShfaqTranskriptenENotave(false);
                  setShfaqPagesatStudentit(false);
                  setPerditeso(Date.now());
                }}
                id={id}
              />
            )}
          {!shfaqTeDhenat &&
            !shfaqVertetiminStudentor &&
            !shfaqTranskriptenENotave &&
            !shfaqPagesatStudentit && (
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
                    <tr>
                      <th>ID Studenti</th>
                      <th>Studenti</th>
                      <th>Fakulteti</th>
                      <th>Kodi Financiar</th>
                      <th>Viti Akademik</th>
                      <th></th>
                    </tr>
                  </thead>

                  <tbody>
                    {filtrimiTeDhenave &&
                      filtrimiTeDhenave.map((p) => {
                        return (
                          <tr key={p.userID}>
                            <td>
                              {p.teDhenatRegjistrimitStudentit &&
                                p.teDhenatRegjistrimitStudentit.idStudenti}
                            </td>
                            <td>
                              {p.emri}{" "}
                              {p.teDhenatPerdoruesit &&
                                p.teDhenatPerdoruesit.emriPrindit}{" "}
                              {p.mbiemri}
                            </td>
                            <td>
                              {p.teDhenatRegjistrimitStudentit &&
                                p.teDhenatRegjistrimitStudentit.departamentet &&
                                p.teDhenatRegjistrimitStudentit.departamentet
                                  .shkurtesaDepartamentit}{" "}
                              -{" "}
                              {p.teDhenatRegjistrimitStudentit &&
                                p.teDhenatRegjistrimitStudentit
                                  .niveliStudimeve &&
                                p.teDhenatRegjistrimitStudentit.niveliStudimeve
                                  .shkurtesaEmritNivelitStudimeve}
                            </td>
                            <td>
                              {p.teDhenatRegjistrimitStudentit &&
                                p.teDhenatRegjistrimitStudentit.kodiFinanciar}
                            </td>
                            <td>
                              {p.teDhenatRegjistrimitStudentit &&
                                p.teDhenatRegjistrimitStudentit
                                  .vitiAkademikRegjistrim}
                            </td>
                            <td>
                              <Button
                                style={{ marginRight: "0.5em" }}
                                variant="success"
                                onClick={() =>
                                  handleShfaqTeDhenat(p.aspNetUserId)
                                }>
                                <FontAwesomeIcon icon={faInfoCircle} />
                              </Button>
                              <Button
                                style={{ marginRight: "0.5em" }}
                                variant="success"
                                onClick={() =>
                                  handleShfaqVertetiminStudentore(p.userID)
                                }>
                                <FontAwesomeIcon icon={faFileArrowDown} />
                              </Button>
                              <Button
                                style={{ marginRight: "0.5em" }}
                                variant="success"
                                onClick={() =>
                                  handleShfaqTranskriptenENotave(p.aspNetUserId)
                                }>
                                <FontAwesomeIcon icon={faScroll} />
                              </Button>
                              <Button
                                style={{ marginRight: "0.5em" }}
                                variant="success"
                                onClick={() =>
                                  handleShfaqPagesatStudentit(p.aspNetUserId)
                                }>
                                <FontAwesomeIcon icon={faReceipt} />
                              </Button>
                            </td>
                          </tr>
                        );
                      })}
                  </tbody>
                </table>
              </>
            )}
        </>
      )}
    </div>
  );
};

export default ListaStudenteve;
