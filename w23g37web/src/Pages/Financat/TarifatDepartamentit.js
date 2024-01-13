/* eslint-disable no-undef */
import React, { useState, useEffect } from "react";
import "../Styles/ProductTables.css";
import Button from "react-bootstrap/Button";
import axios from "axios";
import Mesazhi from "../../Components/TeTjera/layout/Mesazhi";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPenToSquare } from "@fortawesome/free-solid-svg-icons";
import DetajetTarifatDepartamentit from "../../Components/Financat/TarifatDepartamentit/DetajetTarifatDepartamentit";
import { TailSpin } from "react-loader-spinner";
import { Helmet } from "react-helmet";
import { Form, Row, Col } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const TarifatDepartamentit = () => {
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
        if (!rolet.data.rolet.includes("Financa")) {
          navigate("/NukKeniAkses");
        }

        const teDhenat = await axios.get(
          "https://localhost:7251/api/Financat/ShfaqniDepartamentet",
          authentikimi
        );
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

  const filtrimiTeDhenave =
    teDhenat &&
    teDhenat.departamentet &&
    teDhenat.departamentet.filter((departamenti) => {
      return (
        departamenti.emriDepartamentit
          .toLowerCase()
          .includes(kerkimi.toLowerCase()) ||
        departamenti.shkurtesaDepartamentit
          .toLowerCase()
          .includes(kerkimi.toLowerCase()) ||
        departamenti.departamentiID
          .toString()
          .toLowerCase()
          .includes(kerkimi.toLowerCase())
      );
    });

  return (
    <div className="containerDashboardP">
      <Helmet>
        <title>Tarifat e Departamenteve | UBT SMIS</title>
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
            <DetajetTarifatDepartamentit
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
                    <th>ID Departamentit</th>
                    <th>Emri Departamentit</th>
                    <th>Totali Niveleve te Studimit</th>
                    <th>Funksione</th>
                  </tr>
                </thead>

                <tbody>
                  {filtrimiTeDhenave &&
                    filtrimiTeDhenave.map((p) => {
                      return (
                        <tr key={p.departamentiID}>
                          <td>{p.departamentiID}</td>
                          <td>
                            {p.emriDepartamentit} - {p.shkurtesaDepartamentit}
                          </td>
                          <td>
                            {p.tarifatDepartamenti &&
                              p.tarifatDepartamenti.length}
                          </td>
                          <td>
                            <Button
                              style={{ marginRight: "0.5em" }}
                              variant="success"
                              onClick={() =>
                                handleShfaqTeDhenat(p.departamentiID)
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
          )}
        </>
      )}
    </div>
  );
};

export default TarifatDepartamentit;
