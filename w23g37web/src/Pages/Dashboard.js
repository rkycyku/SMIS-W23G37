import React, { useState, useEffect } from "react";
import { Helmet } from "react-helmet";
import "./Styles/Dashboard.css";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { TailSpin } from "react-loader-spinner";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPenToSquare } from "@fortawesome/free-solid-svg-icons";
import Mesazhi from "../Components/TeTjera/layout/Mesazhi";
import PerditesoTeDhenat from "./Gjenerale/TeDhenat/PerditesoTeDhenat";

const Dashboard = () => {
  const [shfaqAdmin, setShfaqAdmin] = useState(false);
  const [teDhenat, setTeDhenat] = useState([]);
  const [perditeso, setPerditeso] = useState("");
  const [loading, setLoading] = useState(true);
  const [shfaqPorosite, setShfaqPorosite] = useState(false);
  const [shfaqDetajet, setShfaqDetajet] = useState(false);
  const [shfaqMesazhet, setShfaqMesazhet] = useState(false);
  const [nrFatures, setNumriFatures] = useState(0);
  const [show, setShow] = useState(false);
  const [edito, setEdito] = useState(false);
  const [emri, setEmri] = useState("");
  const [mbiemri, setMbiemri] = useState("");
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [adresa, setAdresa] = useState("");
  const [nrKontaktit, setNrKontaktit] = useState("");
  const [id, setId] = useState();
  const [mbyllPerditesoTeDhenat, setMbyllPerditesoTeDhenat] = useState(true);
  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");

  const navigate = useNavigate();

  const getID = localStorage.getItem("id");

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    if (getID) {
      const vendosTeDhenat = async () => {
        try {
          const perdoruesi = await axios.get(
            `https://localhost:7251/api/Perdoruesi/shfaqSipasID?idUserAspNet=${getID}`,
            authentikimi
          );
          setTeDhenat(perdoruesi.data);
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

  const perditesoTeDhenat = async () => {
    try {
      const info = {
        emri: emri,
        mbiemri: mbiemri,
        email: email,
        username: username,
        teDhenatPerdoruesit: {
          nrKontaktit: nrKontaktit,
          adresa: adresa,
        },
      };
      fetch("https://localhost:7285/api/Perdoruesi/perditesoPerdorues/" + id, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(info),
      });
    } catch (err) {
      console.log(err);
    } finally {
      setLoading(false);
    }
  };

  const handleShfaqPorosite = () => {
    setShfaqAdmin(false);
    setShfaqDetajet(false);
    setShfaqPorosite(true);
    setShfaqMesazhet(false);
  };

  const handleShfaqAdminDashboard = () => {
    setShfaqAdmin(true);
    setShfaqDetajet(false);
    setShfaqPorosite(false);
    setShfaqMesazhet(false);
  };

  const handleShfaqMesazhet = () => {
    setShfaqAdmin(false);
    setShfaqDetajet(false);
    setShfaqPorosite(false);
    setShfaqMesazhet(true);
  };

  const handleEditoMbyll = () => setEdito(false);

  const handleShow = (ID) => {
    setId(ID);
    setMbyllPerditesoTeDhenat(false);
  };

  return (
    <>
      <Helmet>
        <title>Dashboard | UBT SMIS</title>
      </Helmet>

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
            <h3 class="titulliPershkrim">Te dhenat personale</h3>
            <table>
              <tr>
                <td>
                  <strong>Emri:</strong>
                </td>
                <td>{teDhenat.emri}</td>
              </tr>
              <tr>
                <td>
                  <strong>Mbiemri:</strong>
                </td>
                <td>{teDhenat.mbiemri}</td>
              </tr>
              <tr>
                <td>
                  <strong>Username:</strong>
                </td>
                <td>{teDhenat.username}</td>
              </tr>
              <tr>
                <td>
                  <strong>Email:</strong>
                </td>
                <td>{teDhenat.email}</td>
              </tr>
              <tr>
                <td>
                  <strong>Numri Kontaktit:</strong>
                </td>
                <td>{teDhenat.nrKontaktit}</td>
              </tr>
              <tr>
                <td>
                  <strong>Pozita: </strong>
                </td>
                <td>{teDhenat.rolet && teDhenat.rolet.join(", ")}</td>
              </tr>
              <tr>
                <td>
                  <strong>Adresa: </strong>
                </td>
                <td>
                  {teDhenat.adresa}, {teDhenat.qyteti}, {teDhenat.shteti}{" "}
                  {teDhenat.zipKodi}
                </td>
              </tr>
            </table>
          </div>
        )}
      </div>
    </>
  );
};

export default Dashboard;
