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

const ParaqitjaProvimit = () => {
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

  const [shfaqKalimiLimimitParaqitjes, setShfaqKalimiLimimitParaqitjes] =
    useState(false);
  const [totProvimeveTeMundura, setTotProvimeveTeMundura] = useState(2);

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

        const kontrolloAfatin = await axios.get(
          `https://localhost:7251/api/Studentet/KontrolloAfatinParaqitjesProvimit?data=${Date.now()}`,
          authentikimi
        );

        console.log(Date.now());

        if (kontrolloAfatin.status == 200) {
          const provimet = await axios.get(
            `https://localhost:7251/api/Studentet/ShfaqLendetPerParaqitjeProvimi?studentiID=${getID}&appID=${kontrolloAfatin.data.appid}`,
            authentikimi
          );
          setProvimet(provimet.data);
          setAppData(kontrolloAfatin.data);
          if (
            kontrolloAfatin.data &&
            kontrolloAfatin.data.llojiAfatit == "I Rregullt"
          ) {
            setTotProvimeveTeMundura(10);
          }
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

  const paraqitProvimin = async (lendaID) => {
    const selectedLdpid = selectedLdpIds[lendaID];

    if (
      provimet &&
      provimet.paraqitjaProvimitList &&
      provimet.paraqitjaProvimitList.length == parseInt(totProvimeveTeMundura)
    ) {
      setShfaqKalimiLimimitParaqitjes(true);
    } else {
      await axios.post(
        `https://localhost:7251/api/Studentet/ParaqitniProvimin?studentiID=${getID}&ldPID=${selectedLdpid}&appID=${
          appData && appData.appid
        }`,
        authentikimi
      );

      setPerditeso(Date.now());
    }

    setPerditeso(Date.now());
  };

  return (
    <div className="containerDashboardP">
      <Helmet>
        <title>Paraqit Provimin | UBT SMIS</title>
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
          {kaAfatTeHapur && (
            <Alert variant="success" onClose={() => setShow(false)} dismissible>
              <p>
                Afati i <strong>{appData && appData.afati} </strong> eshte i
                hapur nga{" "}
                <strong>
                  {new Date(
                    appData && appData.dataFillimitAfatit
                  ).toLocaleString("en-GB", {
                    dateStyle: "short",
                    timeStyle: "medium",
                  })}{" "}
                </strong>
                deri me{" "}
                <strong>
                  {new Date(
                    appData && appData.dataMbarimitAfatit
                  ).toLocaleString("en-GB", {
                    dateStyle: "short",
                    timeStyle: "medium",
                  })}
                </strong>{" "}
                ky afat eshte <strong>{appData && appData.llojiAfatit} </strong>{" "}
                dhe keni te drejte te paraqitni deri ne{" "}
                <strong>
                  {appData && appData.llojiAfatit == "I Rregullt" ? "10" : "2"}{" "}
                </strong>{" "}
                provime!
              </p>
            </Alert>
          )}
          {!kaAfatTeHapur && (
            <Alert variant="danger" onClose={() => setShow(false)} dismissible>
              <p>Nuk ka afat te hapur per paraqitje te provimit!</p>
            </Alert>
          )}

          {shfaqKalimiLimimitParaqitjes && (
            <Alert
              variant="danger"
              onClose={() => setShfaqKalimiLimimitParaqitjes(false)}
              dismissible>
              <h5>
                Keni kaluar limitin prej {totProvimeveTeMundura} provime te
                paraqitura!
              </h5>
            </Alert>
          )}
          <table className="tableBig">
            <thead>
              <tr>
                <th>Kodi</th>
                <th>Emri</th>
                <th>Kategoria</th>
                <th>ECTS</th>
                <th>Semestri</th>
                <th>Profesori</th>
                <th></th>
              </tr>
            </thead>

            <tbody>
              {provimet &&
                provimet.lendet &&
                provimet.lendet.map((p) => {
                  return (
                    <tr key={p.lendaID}>
                      <td>{p.kodiLendes}</td>
                      <td>{p.emriLendes}</td>
                      <td>{p.kategoriaLendes}</td>
                      <td>{p.kreditELendes} ECTS</td>
                      <td>Semestri {p.semestriLigjerimit}</td>
                      <td>
                        <select
                          class="form-control"
                          required
                          value={selectedLdpIds[p.lendaID] || ""}
                          onChange={(e) => handleSelectChange(e, p.lendaID)}>
                          <option value="">Zgjedhni Profesorin</option>
                          {p.ldpList &&
                            p.ldpList.map((prof) => {
                              return (
                                <option value={prof && prof.ldpid}>
                                  {prof &&
                                    prof.profesori &&
                                    prof.profesori.emri}{" "}
                                  {prof &&
                                    prof.profesori &&
                                    prof.profesori.mbiemri}
                                </option>
                              );
                            })}
                        </select>
                      </td>
                      <td>
                        <Button
                          id={p.lendaID}
                          variant="success"
                          onClick={() => paraqitProvimin(p.lendaID)}>
                          Paraqitni
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

export default ParaqitjaProvimit;
