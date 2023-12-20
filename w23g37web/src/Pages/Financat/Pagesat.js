/* eslint-disable no-undef */
import React, { useState, useEffect } from "react";
import "../Styles/ProductTables.css";
import Button from "react-bootstrap/Button";
import axios from "axios";
import Mesazhi from "../../Components/TeTjera/layout/Mesazhi";
import ShtoPagesen from "../../Components/Financat/ShtoPagesen";
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

const Pagesat = () => {
  const [pagesat, setPagesat] = useState([]);
  const [id, setId] = useState();
  const [perditeso, setPerditeso] = useState("");
  const [show, setShow] = useState(false);
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
    const shfaqPagesat = async () => {
      try {
        setLoading(true);
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
              {pagesat.map((p) => {
                return (
                  <tr key={p.pagesaID}>
                    <td>{p.llojiPageses}</td>
                    <td>
                      {p.bankat && p.bankat.kodiBankes} - {p.bankat && p.bankat.emriBankes}
                    </td>
                    <td>
                      {p.aplikimiID != null
                        ? p.aplikimetEReja &&
                          p.aplikimetEReja.kodiFinanciar +
                            " - " +
                            p.aplikimetEReja.emri +
                            " " +
                            p.aplikimetEReja.mbiemri
                        : p.perdoruesi &&
                          p.perdoruesi.teDhenatRegjistrimitStudentit &&
                          p.perdoruesi.teDhenatRegjistrimitStudentit
                            .kodiFinanciar != null
                        ? p.perdoruesi && p.perdoruesi.teDhenatRegjistrimitStudentit
                         && p.perdoruesi.teDhenatRegjistrimitStudentit
                            .kodiFinanciar +
                          " - " +
                          p.perdoruesi && p.perdoruesi.emri +
                          " " +
                          p.perdoruesi && p.perdoruesi.mbiemri
                        : p.perdoruesi && p.perdoruesi.username +
                          " - " +
                          p.perdoruesi && p.perdoruesi.emri +
                          " " +
                          p.perdoruesi && p.perdoruesi.mbiemri}
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
