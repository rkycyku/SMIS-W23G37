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
import PagesatStudentit from "../../Components/Studenti/PagesatStudentit/PagesatStudentit";
import { useNavigate } from "react-router-dom";

const PagesatEStudentit = () => {
  const [pagesat, setPagesat] = useState([]);
  const [perditeso, setPerditeso] = useState("");
  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");
  const [loading, setLoading] = useState(false);
  const [id, setId] = useState();

  const [shfaqPagesat, setShfaqPagesat] = useState(false);

  const getToken = localStorage.getItem("token");
  
  const getID = localStorage.getItem("id");
  const navigate = useNavigate();

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const shfaqPagesen = async () => {
      try {
        setLoading(true);
        const rolet = await axios.get(
          `https://localhost:7251/api/Perdoruesi/shfaqSipasID?idUserAspNet=${getID}`,
          authentikimi
        );
        if (!rolet.data.rolet.includes("Student")) {
          navigate("/NukKeniAkses");
        }

        const pagesat = await axios.get(
          `https://localhost:7251/api/Studentet/ShfaqInfoPagesatStudentit?studentiID=${getID}`,
          authentikimi
        );
        setPagesat(pagesat.data);
        setLoading(false);

        if (!rolet.data.rolet.includes("Student")) {
          navigate("/");
        }
      } catch (err) {
        console.log(err);
        setLoading(false);
      }
    };

    shfaqPagesen();
  }, [perditeso]);

  return (
    <div className="containerDashboardP">
      <Helmet>
        <title>Pagesat e Studentit| UBT SMIS</title>
      </Helmet>

      {shfaqMesazhin && (
        <Mesazhi
          setShfaqMesazhin={setShfaqMesazhin}
          pershkrimi={pershkrimiMesazhit}
          tipi={tipiMesazhit}
        />
      )}
      {shfaqPagesat && (
        <PagesatStudentit
          setMbyllTeDhenat={() => {
            setShfaqPagesat(false);
            setPerditeso(Date.now());
          }}
          id={id}
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
          {!shfaqPagesat && (
            <table className="tableBig">
              <thead>
                <tr>
                  <th>#</th>
                  <th>Niveli</th>
                  <th>Drejtimi</th>
                  <th>Kodi Financiar</th>
                  <th>Faturimi</th>
                  <th>Pagesa</th>
                  <th>Mbetja</th>
                  <th>Kartela Analitike</th>
                </tr>
              </thead>

              <tbody>
                <tr key={pagesat && pagesat.aspNetUserID}>
                  <td>1</td>
                  <td>{pagesat && pagesat.niveli}</td>
                  <td>{pagesat && pagesat.drejtimi}</td>
                  <td>{pagesat && pagesat.kodiFinanciar}</td>
                  <td>
                    {parseFloat(pagesat && pagesat.faturimi).toFixed(2)} €
                  </td>
                  <td>{parseFloat(pagesat && pagesat.pagesat).toFixed(2)} €</td>
                  <td>
                    {pagesat &&
                      parseFloat(pagesat.pagesat - pagesat.faturimi).toFixed(
                        2
                      )}{" "}
                    €
                  </td>
                  <td>
                    <Button
                      onClick={() => {
                        setId(pagesat && pagesat.aspNetUserID);
                        setShfaqPagesat(true);
                      }}>
                      <FontAwesomeIcon icon={faInfoCircle} />
                    </Button>
                  </td>
                </tr>
              </tbody>
            </table>
          )}
        </>
      )}
    </div>
  );
};

export default PagesatEStudentit;
