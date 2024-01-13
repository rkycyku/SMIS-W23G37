import React, { useState, useEffect } from "react";
import { Helmet } from "react-helmet";
import "../../Pages/Styles/Dashboard.css";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { TailSpin } from "react-loader-spinner";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faArrowLeft,
  faFileArrowDown,
  faInfoCircle,
  faPenToSquare,
  faReceipt,
  faScroll,
} from "@fortawesome/free-solid-svg-icons";
import Mesazhi from "./../../Components/TeTjera/layout/Mesazhi";
import Tab from "react-bootstrap/Tab";
import Tabs from "react-bootstrap/Tabs";
import { Button } from "react-bootstrap";
import EditoTeDhenatStudentit from "../../Components/Administrata/EditoTeDhenatStudentit";
import VertetimiStudentor from "./VertetimiStudentor";
import TranskriptaNotave from "./TranskriptaNotave/TranskriptaNotave";
import PagesatStudentit from "../Studenti/PagesatStudentit/PagesatStudentit";

const Dashboard = (props) => {
  const [eshteStudent, setEshteStudent] = useState(false);
  const [teDhenat, setTeDhenat] = useState([]);
  const [perditeso, setPerditeso] = useState("");
  const [loading, setLoading] = useState(true);

  const [id, setId] = useState();
  const [show, setShow] = useState(false);
  const [edito, setEdito] = useState(false);
  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");

  const [shfaqVertetiminStudentor, setShfaqVertetiminStudentore] =
    useState(false);
  const [shfaqTranskriptenENotave, setShfaqTranskriptenENotave] =
    useState(false);
  const [shfaqPagesatStudentit, setShfaqPagesatStudentit] = useState(false);

  const [resetoFaqen, setResetoFaqen] = useState(0);

  const navigate = useNavigate();

  const getID = localStorage.getItem("id");

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };
  const handleEditoMbyll = () => setEdito(false);
  const handleShow = () => setShow(true);

  const handleEdito = (id) => {
    setEdito(true);
    setId(id);
  };

  const handleShfaqVertetiminStudentore = (id) => {
    setShfaqVertetiminStudentore(true);
    setShfaqTranskriptenENotave(false);
    setShfaqPagesatStudentit(false);
    setId(id);
  };

  const handleShfaqTranskriptenENotave = (id) => {
    setShfaqVertetiminStudentore(false);
    setShfaqTranskriptenENotave(true);
    setShfaqPagesatStudentit(false);
    setId(id);
  };

  const handleShfaqPagesatStudentit = (id) => {
    setShfaqVertetiminStudentore(false);
    setShfaqTranskriptenENotave(false);
    setShfaqPagesatStudentit(true);
    setId(id);
  };

  useEffect(() => {
    if (getID) {
      const vendosTeDhenat = async () => {
        try {
          const rolet = await axios.get(
            `https://localhost:7251/api/Perdoruesi/shfaqSipasID?idUserAspNet=${props.id}`,
            authentikimi
          );
          const perdoruesi = await axios.get(
            `https://localhost:7251/api/Perdoruesi/ShfaqTeDhenatNgaID?id=${props.id}`,
            authentikimi
          );
          setTeDhenat(perdoruesi.data);

          if (rolet.data.rolet.includes("Student")) {
            setEshteStudent(true);
          }

          setResetoFaqen((prevCount) => prevCount + 1);

          console.log(perdoruesi.data);
        } catch (err) {
          console.log(err);
        } finally {
          setLoading(false);
        }
      };

      vendosTeDhenat();
    } else {
      navigate("/login");
    }
  }, [perditeso]);

  return (
    <>
      {shfaqMesazhin && (
        <Mesazhi
          setShfaqMesazhin={setShfaqMesazhin}
          pershkrimi={pershkrimiMesazhit}
          tipi={tipiMesazhit}
        />
      )}
      {shfaqVertetiminStudentor && (
        <VertetimiStudentor
          setMbyllTeDhenat={() => {
            setShfaqVertetiminStudentore(false);
            setShfaqTranskriptenENotave(false);
            setShfaqPagesatStudentit(false);
            setPerditeso(Date.now());
          }}
          id={id}
        />
      )}
      {shfaqTranskriptenENotave && (
        <TranskriptaNotave
          setMbyllTeDhenat={() => {
            setShfaqVertetiminStudentore(false);
            setShfaqTranskriptenENotave(false);
            setShfaqPagesatStudentit(false);
            setPerditeso(Date.now());
          }}
          id={id}
        />
      )}
      {shfaqPagesatStudentit && (
        <PagesatStudentit
          setMbyllTeDhenat={() => {
            setShfaqVertetiminStudentore(false);
            setShfaqTranskriptenENotave(false);
            setShfaqPagesatStudentit(false);
            setPerditeso(Date.now());
          }}
          id={id}
        />
      )}
      {edito && (
        <EditoTeDhenatStudentit
          show={handleShow}
          hide={handleEditoMbyll}
          id={id}
          shfaqmesazhin={() => setShfaqMesazhin(true)}
          perditesoTeDhenat={() => setPerditeso(Date.now())}
          setTipiMesazhit={setTipiMesazhit}
          setPershkrimiMesazhit={setPershkrimiMesazhit}
        />
      )}
      <div className="dashboard">
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
          <div class="containerDashboard">
            <h3 class="titulliPershkrim">Te dhenat per studentin</h3>
            <Tabs
              defaultActiveKey="InformatatPersonale"
              id="uncontrolled-tab-example"
              className="mb-3">
              <Tab eventKey="InformatatPersonale" title="Informatat Personale">
                <table>
                  <tr>
                    <td>
                      <strong>
                        {!eshteStudent ? "ID:" : "ID e Studentit:"}
                      </strong>
                    </td>
                    <td>
                      {!eshteStudent
                        ? teDhenat &&
                          teDhenat.teDhenatPerdoruesit &&
                          teDhenat.teDhenatPerdoruesit.userID
                        : teDhenat &&
                          teDhenat.teDhenatRegjistrimitStudentit &&
                          teDhenat.teDhenatRegjistrimitStudentit.idStudenti}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Përdoruesi:</strong>
                    </td>
                    <td>{teDhenat && teDhenat.email}</td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Nr. Personal:</strong>
                    </td>
                    <td>
                      {teDhenat &&
                        teDhenat.teDhenatPerdoruesit &&
                        teDhenat.teDhenatPerdoruesit.nrPersonal}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Emri:</strong>
                    </td>
                    <td>{teDhenat && teDhenat.emri}</td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Emri i prindit:</strong>
                    </td>
                    <td>
                      {teDhenat &&
                        teDhenat.teDhenatPerdoruesit &&
                        teDhenat.teDhenatPerdoruesit.emriPrindit}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Mbiemri: </strong>
                    </td>
                    <td>{teDhenat && teDhenat.mbiemri}</td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Datëlindja: </strong>
                    </td>
                    <td>
                      {new Date(
                        teDhenat &&
                          teDhenat.teDhenatPerdoruesit &&
                          teDhenat.teDhenatPerdoruesit.dataLindjes
                      ).toLocaleDateString("en-GB", { dateStyle: "short" })}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Gjinia: </strong>
                    </td>
                    <td>
                      {teDhenat &&
                        teDhenat.teDhenatPerdoruesit &&
                        teDhenat.teDhenatPerdoruesit.gjinia}
                    </td>
                  </tr>
                </table>
              </Tab>
              <Tab eventKey="Kontakti" title="Kontakti">
                <table>
                  <tr>
                    <td>
                      <strong>Shteti:</strong>
                    </td>
                    <td>
                      {teDhenat &&
                        teDhenat.teDhenatPerdoruesit &&
                        teDhenat.teDhenatPerdoruesit.shteti}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Vendbanimi:</strong>
                    </td>
                    <td>
                      {teDhenat &&
                        teDhenat.teDhenatPerdoruesit &&
                        teDhenat.teDhenatPerdoruesit.qyteti}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Adresa:</strong>
                    </td>
                    <td>
                      {teDhenat &&
                        teDhenat.teDhenatPerdoruesit &&
                        teDhenat.teDhenatPerdoruesit.adresa}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <strong>ZipKodi:</strong>
                    </td>
                    <td>
                      {teDhenat &&
                        teDhenat.teDhenatPerdoruesit &&
                        teDhenat.teDhenatPerdoruesit.zipKodi}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Telefon:</strong>
                    </td>
                    <td>
                      {teDhenat &&
                        teDhenat.teDhenatPerdoruesit &&
                        teDhenat.teDhenatPerdoruesit.nrKontaktit}
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <strong>Emaili Privat: </strong>
                    </td>
                    <td>
                      {teDhenat &&
                        teDhenat.teDhenatPerdoruesit &&
                        teDhenat.teDhenatPerdoruesit.emailPersonal}
                    </td>
                  </tr>
                </table>
              </Tab>
              {eshteStudent && (
                <Tab
                  eventKey="TeDhenatRegjistrimit"
                  title="Te Dhenat Regjistrimit">
                  <table>
                    <tr>
                      <td>
                        <strong>Kodi Financiar:</strong>
                      </td>
                      <td>
                        {teDhenat &&
                          teDhenat.teDhenatRegjistrimitStudentit &&
                          teDhenat.teDhenatRegjistrimitStudentit.kodiFinanciar}
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <strong>Fakulteti:</strong>
                      </td>
                      <td>
                        {teDhenat &&
                          teDhenat.teDhenatRegjistrimitStudentit &&
                          teDhenat.teDhenatRegjistrimitStudentit
                            .departamentet &&
                          teDhenat.teDhenatRegjistrimitStudentit.departamentet
                            .emriDepartamentit}
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <strong>Niveli:</strong>
                      </td>
                      <td>
                        {teDhenat &&
                          teDhenat.teDhenatRegjistrimitStudentit &&
                          teDhenat.teDhenatRegjistrimitStudentit
                            .niveliStudimeve &&
                          teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve
                            .emriNivelitStudimeve}
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <strong>Regjistruar në vitin akademik:</strong>
                      </td>
                      <td>
                        {teDhenat &&
                          teDhenat.teDhenatRegjistrimitStudentit &&
                          teDhenat.teDhenatRegjistrimitStudentit
                            .vitiAkademikRegjistrim}
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <strong>Data e regjistrimit:</strong>
                      </td>
                      <td>
                        {new Date(
                          teDhenat &&
                            teDhenat.teDhenatRegjistrimitStudentit &&
                            teDhenat.teDhenatRegjistrimitStudentit
                              .dataRegjistrimit
                        ).toLocaleDateString("en-GB", { dateStyle: "short" })}
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <strong>Lloji i regjistrimit: </strong>
                      </td>
                      <td>
                        {teDhenat &&
                          teDhenat.teDhenatRegjistrimitStudentit &&
                          teDhenat.teDhenatRegjistrimitStudentit
                            .llojiRegjistrimit}
                      </td>
                    </tr>
                  </table>
                </Tab>
              )}
            </Tabs>
            <div className="mt-3">
              <Button
                style={{ marginRight: "0.5em" }}
                variant="success"
                onClick={() => props.setMbyllTeDhenat()}>
                <FontAwesomeIcon icon={faArrowLeft} /> Kthehu
              </Button>
              <Button
                style={{ marginRight: "0.5em" }}
                variant="success"
                onClick={() => handleEdito(props.id)}>
                Perditeso te Dhenat <FontAwesomeIcon icon={faPenToSquare} />
              </Button>
              <Button
                style={{ marginRight: "0.5em" }}
                variant="success"
                onClick={() =>
                  handleShfaqVertetiminStudentore(teDhenat && teDhenat.userID)
                }>
                Vertetimi Studentor <FontAwesomeIcon icon={faFileArrowDown} />
              </Button>
              <Button
                style={{ marginRight: "0.5em" }}
                variant="success"
                onClick={() =>
                  handleShfaqTranskriptenENotave(
                    teDhenat && teDhenat.aspNetUserId
                  )
                }>
                Transkripta e Notave <FontAwesomeIcon icon={faScroll} />
              </Button>
              <Button
                style={{ marginRight: "0.5em" }}
                variant="success"
                onClick={() =>
                  handleShfaqPagesatStudentit(teDhenat && teDhenat.aspNetUserId)
                }>
                Kartela Analitike <FontAwesomeIcon icon={faReceipt} />
              </Button>
            </div>
          </div>
        )}
      </div>
    </>
  );
};

export default Dashboard;
