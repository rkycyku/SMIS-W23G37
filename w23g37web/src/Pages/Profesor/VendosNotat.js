/* eslint-disable no-undef */
import React, { useState, useEffect } from "react";
import "../Styles/ProductTables.css";
import Button from "react-bootstrap/Button";
import axios from "axios";
import Mesazhi from "../../Components/TeTjera/layout/Mesazhi";
import Form from "react-bootstrap/Form";
import { Col, Row } from "react-bootstrap";
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

const VendosNotat = () => {
  const [lendet, setLendet] = useState([]);
  const [id, setId] = useState();
  const [perditeso, setPerditeso] = useState("");
  const [show, setShow] = useState(false);
  const [edito, setEdito] = useState(false);
  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");
  const [loading, setLoading] = useState(false);
  const [selectedLdpIds, setSelectedLdpIds] = useState({});

  const [studentat, setStudentat] = useState([]);
  const [departamentetPerLenden, setDeparamentetPerLenden] = useState([]);

  const [departamentiID, setDepartamentiID] = useState();
  const [paraqitjaPID, setParaqitjaPID] = useState();

  const getToken = localStorage.getItem("token");
  const getID = localStorage.getItem("id");
  const navigate = useNavigate();

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const shfaqLendet = async () => {
      try {
        setLoading(true);
        const rolet = await axios.get(
          `https://localhost:7251/api/Perdoruesi/shfaqSipasID?idUserAspNet=${getID}`,
          authentikimi
        );
        if (!rolet.data.rolet.includes("Profesor")) {
          navigate("/NukKeniAkses");
        }

        const lendet = await axios.get(
          `https://localhost:7251/api/Profesor/ShfaqLendetProfesorit?profesoriID=${getID}`,
          authentikimi
        );

        setLendet(lendet.data);
        setLoading(false);
      } catch (err) {
        console.log(err);
        setLoading(false);
      }
    };

    shfaqLendet();
  }, [perditeso]);

  useEffect(() => {
    const shfaqDepartamentetPerLende = async () => {
      try {
        setLoading(true);

        const departamentetPerLenden = await axios.get(
          `https://localhost:7251/api/Profesor/ShfaqDepartamentinPerLendetProfesorit?profesoriID=${getID}&lendaID=${id}`,
          authentikimi
        );

        setDeparamentetPerLenden(departamentetPerLenden.data.departamentet);
        setLoading(false);
      } catch (err) {
        console.log(err);
        setLoading(false);
      }
    };

    shfaqDepartamentetPerLende();
  }, [id, perditeso]);

  useEffect(() => {
    const shfaqStudentat = async () => {
      try {
        setLoading(true);

        const studentet = await axios.get(
          `https://localhost:7251/api/Profesor/ShfaqStudentetQeKaneParaqiturProvimin?profesoriID=${getID}&lendaID=${id}&departamentiID=${departamentiID}`,
          authentikimi
        );

        setStudentat(studentet.data);
        setLoading(false);
      } catch (err) {
        console.log(err);
        setLoading(false);
      }
    };

    shfaqStudentat();
  }, [departamentiID, perditeso]);

  const handleClose = () => {
    setShow(false);
  };
  const handleShow = () => setShow(true);

  const handleEdito = (id) => {
    setEdito(true);
    setId(id);
  };

  const handleSelectChange = (event, paraqitjaProvimitID) => {
    const { value } = event.target;
    setSelectedLdpIds((prevState) => ({
      ...prevState,
      [paraqitjaProvimitID]: value,
    }));
  };

  const handleLendaChange = (event) => {
    setId(event.target.value);
    setDepartamentiID(null);
    setStudentat([]);
  };

  const handleDepartamentiChange = (event) => {
    setDepartamentiID(event.target.value);
  };

  const vendosNoten = async (paraqitjaProvimitID) => {
    const nota = selectedLdpIds[paraqitjaProvimitID];

    const vendosNoten = await axios.post(
      `https://localhost:7251/api/Profesor/VendosNotenStudentit?paraqitjaProvimitID=${paraqitjaProvimitID}&nota=${
        nota.length >= 3 ? nota.split(" - ")[0].trim() : nota
      }&statusi=${nota.length >= 3 ? nota.split(" - ")[1].trim() : "Rregullt"}`,
      authentikimi
    );

    if (vendosNoten.status != 200) {
      setShfaqMesazhin(true);
      setTipiMesazhit("danger");
      setPershkrimiMesazhit(
        "Ndodhi nje gabime gjate vendosjes te notes, Ju lutem kontaktoni me supportin!"
      );
    }

    setPerditeso(Date.now());
  };

  const perditesoNoten = async (paraqitjaProvimitID) => {
    const nota = selectedLdpIds[paraqitjaProvimitID];

    const vendosNoten = await axios.put(
      `https://localhost:7251/api/Profesor/PerditesoNotenStudentit?paraqitjaProvimitID=${paraqitjaProvimitID}&nota=${
        nota.length >= 3 ? nota.split(" - ")[0].trim() : nota
      }&statusi=${nota.length >= 3 ? nota.split(" - ")[1].trim() : "Rregullt"}`,
      authentikimi
    );

    if (vendosNoten.status != 200) {
      setShfaqMesazhin(true);
      setTipiMesazhit("danger");
      setPershkrimiMesazhit(
        "Ndodhi nje gabime gjate perditesimin te notes, Ju lutem kontaktoni me supportin!"
      );
    }

    setPerditeso(Date.now());
  };

  const refuzoNoten = async (paraqitjaProvimitID) => {
    const nota = selectedLdpIds[paraqitjaProvimitID];

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

  const [showD, setShowD] = useState(false);

  const handleCloseD = () => setShowD(false);
  const handleShowD = (id) => {
    setParaqitjaPID(id);
    setShowD(true);
  };

  return (
    <div className="containerDashboardP">
      <Helmet>
        <title>Vendos Notat | UBT SMIS</title>
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
          <Form.Select
            required
            value={id}
            onChange={(e) => handleLendaChange(e)}
            className="mb-1">
            <option value="">Zgjedhni Lenden</option>
            {lendet &&
              lendet.lendet &&
              lendet.lendet.map((lenda) => (
                <option key={lenda.lendaID} value={lenda.lendaID}>
                  {lenda.kodiLendes} - {lenda.emriLendes}
                </option>
              ))}
          </Form.Select>
          <Form.Select
            required
            value={departamentiID}
            onChange={(e) => handleDepartamentiChange(e)}
            className="mb-1">
            <option value="">Zgjedhni Departamentin</option>
            {departamentetPerLenden.map((departamenti) => {
              return (
                <option value={departamenti.departamentiID}>
                  {departamenti.emriDepartamentit} -{" "}
                  {departamenti.shkurtesaDepartamentit}
                </option>
              );
            })}
          </Form.Select>

          <table className="tableBig">
            <thead>
              <tr>
                <th>ID Studenti</th>
                <th>Studenti</th>
                <th>Nota</th>
                <th></th>
              </tr>
            </thead>

            <tbody>
              {studentat.map((p) => {
                return (
                  <tr key={p.paraqitjaProvimitID}>
                    <td>{p.studentiIDFK}</td>
                    <td>{p.studenti}</td>
                    <td>
                      <select
                        class="form-control"
                        required
                        value={
                          selectedLdpIds[p.paraqitjaProvimitID]
                            ? selectedLdpIds[p.paraqitjaProvimitID]
                            : p.nota != null
                            ? p.nota
                            : "VendosNoten"
                        }
                        onChange={(e) =>
                          handleSelectChange(e, p.paraqitjaProvimitID)
                        }
                        disabled={p.statusiNotes == "ERefuzuar"}>
                        <option
                          selected={p.statusiNotes == "ERefuzuar"}
                          hidden={p.statusiNotes != "ERefuzuar"}
                          value="">
                          {p.nota}
                        </option>
                        <option value="VendosNoten" disabled>
                          Vendos Noten
                        </option>
                        <option value="10">10</option>
                        <option value="9">9</option>
                        <option value="8">8</option>
                        <option value="7">7</option>
                        <option value="6">6</option>
                        <option value="5">5</option>
                        <option>5 - Abstenim</option>
                        <option>0 - JoPrezent</option>
                      </select>
                    </td>
                    <td>
                      <Button
                        className="mx-1"
                        id={p.lendaID}
                        variant="success"
                        onClick={() => vendosNoten(p.paraqitjaProvimitID)}
                        disabled={
                          p.nota != null || p.statusiNotes == "ERefuzuar"
                        }>
                        Vendos Noten
                      </Button>
                      <Button
                        className="mx-1"
                        id={p.lendaID}
                        variant="success"
                        onClick={() => perditesoNoten(p.paraqitjaProvimitID)}
                        disabled={
                          p.nota == null || p.statusiNotes == "ERefuzuar"
                        }>
                        Perditeso Noten
                      </Button>
                      <Button
                        id={p.lendaID}
                        variant="success"
                        onClick={() => handleShowD(p.paraqitjaProvimitID)}
                        disabled={
                          p.nota == null ||
                          p.nota === "5" ||
                          p.nota === "0" ||
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

export default VendosNotat;
