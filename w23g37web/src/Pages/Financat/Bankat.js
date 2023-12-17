/* eslint-disable no-undef */
import React, { useState, useEffect } from "react";
import "../Styles/ProductTables.css";
import Button from "react-bootstrap/Button";
import axios from "axios";
import Mesazhi from "../../Components/TeTjera/layout/Mesazhi";
import ShtoBanken from "../../Components/Financat/ShtoBanken";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBan, faPenToSquare, faPlus, faXmark, faCheck, faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import EditoBanken from "../../Components/Financat/EditoBanken";
import Modal from "react-bootstrap/Modal";
import { TailSpin } from 'react-loader-spinner';
import { Helmet } from "react-helmet";

const Bankat = () => {
  const [bankat, setBankat] = useState([]);
  const [id, setId] = useState();
  const [perditeso, setPerditeso] = useState("");
  const [show, setShow] = useState(false);
  const [edito, setEdito] = useState(false);
  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");
  const [loading, setLoading] = useState(false);

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const shfaqBankat = async () => {
      try {
        setLoading(true);
        const bankat = await axios.get(
          "https://localhost:7251/api/Financat/ShfaqBankat", authentikimi
        );
        setBankat(bankat.data);
        setLoading(false);
      } catch (err) {
        console.log(err);
        setLoading(false);
      }
    };

    shfaqBankat();
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
      await axios.delete(`https://localhost:7251/api/Financat/FshiniBanken?bankaID=` + id, authentikimi);
      setTipiMesazhit("success");
      setPershkrimiMesazhit("Banka u fshi me sukses!");
      setPerditeso(Date.now());
      setShfaqMesazhin(true);
      setShowD(false);
    } catch (err) {
      console.error(err);
      setTipiMesazhit("danger");
      setPershkrimiMesazhit("Ndodhi nje gabim gjate fshirjes se Bankes!");
      setPerditeso(Date.now());
      setShfaqMesazhin(true);
      setShowD(false);
    }
  }

  return (
    <div className="containerDashboardP">
       <Helmet>
        <title>Bankat | UBT SMIS</title>
      </Helmet>
      {show && (
        <ShtoBanken
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
      {edito && (
        <EditoBanken
          show={handleShow}
          hide={handleEditoMbyll}
          id={id}
          shfaqmesazhin={() => setShfaqMesazhin(true)}
          perditesoTeDhenat={() => setPerditeso(Date.now())}
          setTipiMesazhit={setTipiMesazhit}
          setPershkrimiMesazhit={setPershkrimiMesazhit}
        />
      )}
      <Modal show={showD} onHide={handleCloseD}>
        <Modal.Header closeButton>
          <Modal.Title style={{ color: "red" }}>Largo Banken</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <h6>A jeni te sigurt qe deshironi ta fshini kete Banke?</h6>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseD}>
            Anulo <FontAwesomeIcon icon={faXmark} />
          </Button>
          <Button variant="danger" onClick={handleDelete}>
            Largo Banken <FontAwesomeIcon icon={faBan} />
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
      ) : <>
        <Button className="mb-3 Butoni" onClick={handleShow}>
          Shto Banken <FontAwesomeIcon icon={faPlus} />
        </Button>
        <table className="tableBig">
          <thead>
            <tr>
              <th>Emri Bankes</th>
              <th>Kodi Bankes</th>
              <th>Numri Llogaris</th>
              <th>Adresa Bankes</th>
              <th>Valuta</th>
              <th>Lloji i Bankes</th>
              <th>Funksione</th>
            </tr>
          </thead>

          <tbody>
            {bankat.map((p) => {
              return (
                <tr key={p.bankaID}>
                  <td>{p.emriBankes}</td>
                  <td>{p.kodiBankes}</td>
                  <td>{p.ibanFillimi}{p.numriLlogaris}</td>
                  <td>{p.adresaBankes}</td>
                  <td>{p.valuta}</td>
                  <td>{p.llojiBankes}</td>
                  <td>
                    <Button
                      style={{ marginRight: "0.5em" }}
                      variant="success"
                      onClick={() => handleEdito(p.bankaID)}
                    >
                      <FontAwesomeIcon icon={faPenToSquare} />
                    </Button>
                    <Button
                      variant="danger"
                      onClick={() => handleShowD(p.bankaID)}
                    >
                      <FontAwesomeIcon icon={faBan} />
                    </Button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </>
      }
    </div>
  );
};

export default Bankat;
