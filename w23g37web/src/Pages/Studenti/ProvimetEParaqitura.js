/* eslint-disable no-undef */
import React, { useState, useEffect } from "react";
import "../Styles/ProductTables.css";
import Button from "react-bootstrap/Button";
import axios from "axios";
import Mesazhi from "../../Components/TeTjera/layout/Mesazhi";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faBan,
  faPenToSquare,
  faPlus,
  faXmark,
  faCheck,
  faInfoCircle,
} from "@fortawesome/free-solid-svg-icons";
import Modal from "react-bootstrap/Modal";
import { TailSpin } from "react-loader-spinner";
import { Helmet } from "react-helmet";
import { Alert } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const ProvimetEParaqitura = () => {
  const [provimet, setProvimet] = useState([]);
  const [id, setId] = useState();
  const [perditeso, setPerditeso] = useState("");
  const [show, setShow] = useState(false);
  const [edito, setEdito] = useState(false);
  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");
  const [loading, setLoading] = useState(false);
  const [appData, setAppData] = useState([]);
  const [kaAfatTeHapur, setKaAfatTeHapur] = useState(false);
  const [selectedLdpIds, setSelectedLdpIds] = useState({});

  const getToken = localStorage.getItem("token");
  const getID = localStorage.getItem("id");
  const navigate = useNavigate();

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const shfaqprovimet = async () => {
      try {
        setLoading(true);
        const rolet = await axios.get(
          `https://localhost:7251/api/Perdoruesi/shfaqSipasID?idUserAspNet=${getID}`,
          authentikimi
        );
        if (!rolet.data.rolet.includes("Student")) {
          navigate("/NukKeniAkses");
        }
        const provimet = await axios.get(
          `https://localhost:7251/api/Studentet/ShfaqProvimetEParaqitura?studentiID=${getID}`,
          authentikimi
        );
        setProvimet(provimet.data.provimet);
        setAppData(provimet.data.app);

        if (provimet.status == 200) {
          setKaAfatTeHapur(true);
        } else {
          setKaAfatTeHapur(false);
        }
        setLoading(false);

        if (rolet.data.rolet.includes("Student")) {
          const pagesat = await axios.get(
            `https://localhost:7251/api/Studentet/ShfaqInfoPagesatStudentit?studentiID=${getID}`,
            authentikimi
          );

          if (pagesat.data.mbetja < 0) {
            navigate("/PagesatEPaPerfunduara");
          }
        }
      } catch (err) {
        console.log(err);
        setLoading(false);
      }
    };

    shfaqprovimet();
  }, [perditeso]);

  const handleClose = () => {
    setShow(false);
  };
  const handleShow = () => setShow(true);

  const handleEdito = (id) => {
    setEdito(true);
    setId(id);
  };

  const handleSelectChange = (event, lendaID) => {
    const { value } = event.target;
    setSelectedLdpIds((prevState) => ({
      ...prevState,
      [lendaID]: value,
    }));
  };

  const anuloParaqitjenProvimit = async (paraqitjaProvimitID) => {
    const anuloParaqitjen = await axios.delete(
      `https://localhost:7251/api/Studentet/AnuloParaqitjenProvimit?paraqitjaProvimitID=${paraqitjaProvimitID}`,
      authentikimi
    );

    if (anuloParaqitjen.status != 200) {
      setShfaqMesazhin(true);
      setTipiMesazhit("danger");
      setPershkrimiMesazhit(
        "Ndodhi nje gabime gjate anulimit te paraqitjes se provimit, Ju lutem kontaktoni me supportin!"
      );
    }

    setPerditeso(Date.now());
    setShowD(false);
    setParaqitjaPID();
  };

  const refuzoNoten = async (paraqitjaProvimitID) => {
    const refuzoNoten = await axios.put(
      `https://localhost:7251/api/Profesor/RefuzoNotenStudentit?paraqitjaProvimitID=${paraqitjaProvimitID}`,
      authentikimi
    );

    if (refuzoNoten.status != 200) {
      setShfaqMesazhin(true);
      setTipiMesazhit("danger");
      setPershkrimiMesazhit(
        "Ndodhi nje gabime gjate refuzimit te notes, Ju lutem kontaktoni me supportin!"
      );
    }

    setPerditeso(Date.now());
    setShowD(false);
    setParaqitjaPID();
  };

  const [paraqitjaPID, setParaqitjaPID] = useState();
  const [showD, setShowD] = useState(false);

  const handleCloseD = () => setShowD(false);
  const handleShowD = (id) => {
    setParaqitjaPID(id);
    setShowD(true);
  };

  return (
    <div className="containerDashboardP">
      <Helmet>
        <title>Provimet e Paraqitura | UBT SMIS</title>
      </Helmet>

      {shfaqMesazhin && (
        <Mesazhi
          setShfaqMesazhin={setShfaqMesazhin}
          pershkrimi={pershkrimiMesazhit}
          tipi={tipiMesazhit}
        />
      )}
      <Modal show={showD} onHide={handleCloseD}>
        <Modal.Header closeButton>
          <Modal.Title style={{ color: "red" }}>Refuzo Noten</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <h6>A jeni te sigurt qe deshironi ta refuzoni kete Note?</h6>
          <p>
            Pasi nota do te refuzohet ajo nuk do te kete mundesi te vendoset
            prape ne kete afat!
          </p>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseD}>
            Anulo <FontAwesomeIcon icon={faXmark} />
          </Button>
          <Button variant="danger" onClick={() => refuzoNoten(paraqitjaPID)}>
            Refuzo Noten <FontAwesomeIcon icon={faBan} />
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
          <table className="tableBig">
            <thead>
              <tr>
                <th>Kodi</th>
                <th>Lenda</th>
                <th>Kategoria</th>
                <th>Profesori</th>
                <th>Nota</th>
                <th>Statusi Notes</th>
                <th>Data Vendosjes Notes</th>
                <th>Anulo Paraqitjen e Provimit</th>
                <th>Refuzo Noten</th>
              </tr>
            </thead>

            <tbody>
              {provimet &&
                provimet.map((p) => {
                  return (
                    <tr key={p.kodiLendes}>
                      <td>{p.kodiLendes}</td>
                      <td>{p.emriLendes}</td>
                      <td>{p.kategoriaLendes}</td>
                      <td>{p.profesori}</td>
                      <td>{p.nota != null ? p.nota : "-"}</td>
                      <td>{p.statusiNotes != null ? p.statusiNotes : "-"}</td>
                      <td>
                        {p.dataVendosjesNotes != null
                          ? new Date(p.dataVendosjesNotes).toLocaleString(
                              "en-GB",
                              {
                                dateStyle: "short",
                                timeStyle: "medium",
                              }
                            )
                          : "-"}
                      </td>
                      <td>
                        <Button
                          id={p.lendaID}
                          variant="success"
                          onClick={() =>
                            anuloParaqitjenProvimit(p.paraqitjaProvimitID)
                          }
                          disabled={
                            (appData &&
                              new Date(appData.dataMbarimitAfatit) <
                                new Date()) ||
                            p.nota != null
                          }>
                          Anulo Paraqitjen
                        </Button>
                      </td>
                      <td>
                        <Button
                          id={p.lendaID}
                          variant="success"
                          onClick={() => handleShowD(p.paraqitjaProvimitID)}
                          disabled={
                            p.nota == null ||
                            parseInt(p.nota) <= 5 ||
                            new Date(
                              new Date(
                                new Date(p.dataVendosjesNotes).getTime() +
                                  2 * 24 * 60 * 60 * 1000
                              )
                            ) <= new Date() ||
                            p.statusiNotes == "ERefuzuar"
                          }>
                          Refuzo Noten
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

export default ProvimetEParaqitura;
