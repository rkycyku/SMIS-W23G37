/* eslint-disable no-undef */
import React, { useState, useEffect } from "react";
import "../Styles/ProductTables.css";
import Button from "react-bootstrap/Button";
import axios from "axios";
import Mesazhi from "../../Components/TeTjera/layout/Mesazhi";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faFileArrowDown, faInfoCircle, faPenToSquare } from "@fortawesome/free-solid-svg-icons";
import DetajetAplikimit from "./../../Components/Administrata/DetajetAplikimit";
import { TailSpin } from "react-loader-spinner";
import { Helmet } from "react-helmet";
import { Form, Row, Col } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const AplikimetEReja = () => {
  const [teDhenat, setTeDhenat] = useState([]);
  const [id, setId] = useState();
  const [perditeso, setPerditeso] = useState("");
  const [shfaqTeDhenat, setShfaqTeDhenat] = useState(false);
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
    const shfaqDepartamentet = async () => {
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
          "https://localhost:7251/api/Administrata/ShfaqAplikimetEReja",
          authentikimi
        );
        console.log(teDhenat.data);
        setTeDhenat(teDhenat.data);
        setLoading(false);
      } catch (err) {
        console.log(err);
        setLoading(false);
      }
    };

    shfaqDepartamentet();
  }, [perditeso]);

  const handleShfaqTeDhenat = (id) => {
    setShfaqTeDhenat(true);
    setId(id);
  };

  const handleKerkimi = (event) => {
    setKerkimi(event.target.value);
  };

  const filtrimiTeDhenave = teDhenat.filter((student) => {
    const emriPlote =
      `${student.emri} ${student.emriPrindit} ${student.mbiemri}`.toLowerCase();

    return (
      emriPlote.includes(kerkimi.toLowerCase()) ||
      student.nrPersonal.toLowerCase().includes(kerkimi.toLowerCase()) ||
      (student.niveliStudimeve &&
        student.niveliStudimeve.shkurtesaEmritNivelitStudimeve
          .toLowerCase()
          .includes(kerkimi.toLowerCase())) ||
      (student.niveliStudimeve &&
        student.niveliStudimeve.emriNivelitStudimeve
          .toLowerCase()
          .includes(kerkimi.toLowerCase())) ||
      (student.departamentet &&
        student.departamentet.shkurtesaDepartamentit
          .toLowerCase()
          .includes(kerkimi.toLowerCase())) ||
      (student.departamentet &&
        student.departamentet.emriDepartamentit
          .toLowerCase()
          .includes(kerkimi.toLowerCase()))
    );
  });

  return (
    <div className="containerDashboardP">
      <Helmet>
        <title>Lista Aplikimeve te Reja | UBT SMIS</title>
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
          {shfaqTeDhenat && (
            <DetajetAplikimit
              setMbyllTeDhenat={() => {
                setShfaqTeDhenat(false);
                setPerditeso(Date.now());
              }}
              id={id}
            />
          )}
          {!shfaqTeDhenat && (
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
                    <th>Numri Aplikimit</th>
                    <th>Studenti</th>
                    <th>Nr. Personal</th>
                    <th>Kodi Financiar</th>
                    <th>Departamenti</th>
                    <th>Niveli Studimeve</th>
                    <th></th>
                  </tr>
                </thead>

                <tbody>
                  {filtrimiTeDhenave &&
                    filtrimiTeDhenave.map((p, index) => {
                      return (
                        <tr key={index}>
                          <td>{index + 1}</td>
                          <td>
                            {p.emri} {p.emriPrindit} {p.mbiemri}
                          </td>
                          <td>{p.nrPersonal}</td>
                          <td>{p.kodiFinanciar}</td>
                          <td>
                            {p.departamentet &&
                              p.departamentet.shkurtesaDepartamentit}
                          </td>
                          <td>
                            {p.niveliStudimeve &&
                              p.niveliStudimeve.shkurtesaEmritNivelitStudimeve}
                          </td>
                          <td>
                            <Button
                              style={{ marginRight: "0.5em" }}
                              variant="success"
                              onClick={() => handleShfaqTeDhenat(p.aplikimiID)}>
                              <FontAwesomeIcon icon={faFileArrowDown} />
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

export default AplikimetEReja;
