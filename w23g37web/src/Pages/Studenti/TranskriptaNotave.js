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

const TranskriptaNotave = () => {
  const [notat, setNotat] = useState([]);
  const [perditeso, setPerditeso] = useState("");
  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");
  const [loading, setLoading] = useState(false);

  const navigate = useNavigate();

  const getToken = localStorage.getItem("token");
  const getID = localStorage.getItem("id");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const shfaqnotat = async () => {
      try {
        setLoading(true);

        const rolet = await axios.get(
          `https://localhost:7251/api/Perdoruesi/shfaqSipasID?idUserAspNet=${getID}`,
          authentikimi
        );
        if (!rolet.data.rolet.includes("Student")) {
          navigate("/NukKeniAkses");
        }

        const notat = await axios.get(
          `https://localhost:7251/api/Studentet/ShfaqTranskriptenNotaveStudentit?studentiID=${getID}`,
          authentikimi
        );
        setNotat(notat.data.notatStudentiList);
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

    shfaqnotat();
  }, [perditeso]);

  let totalNota = 0;
  let totalKredi = 0;
  let notaMesatare = 0;

  if (notat) {
    notat.forEach((p) => {
      const nota = p.nota || 0;
      const lenda = p.lenda || {};
      const kreditELendes = lenda.kreditELendes || 0;

      totalNota += nota;
      totalKredi += kreditELendes;
    });

    notaMesatare = totalNota / notat.length;
  }

  return (
    <div className="containerDashboardP">
      <Helmet>
        <title>Transkripta Notave | UBT SMIS</title>
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
          <table className="tableBig">
            <thead>
              <tr>
                <th>#</th>
                <th>Kodi</th>
                <th>Lenda</th>
                <th>ECTS (Kredit)</th>
                <th>Kategoria</th>
                <th>Nota</th>
                <th>Nota Shkronje</th>
                <th>Statusi Notes</th>
              </tr>
            </thead>

            <tbody>
              {notat &&
                notat.map((p, index) => {
                  return (
                    <tr key={p.notaStudentiID}>
                      <td>{index + 1}</td>
                      <td>{p.lenda && p.lenda.kodiLendes}</td>
                      <td>{p.lenda && p.lenda.emriLendes}</td>
                      <td>{p.lenda && p.lenda.kreditELendes}</td>
                      <td>{p.lenda && p.lenda.kategoriaLendes}</td>
                      <td>{p.nota}</td>
                      <td>
                        {p.nota == 10
                          ? "A"
                          : p.nota == 9
                          ? "B"
                          : p.nota == 8
                          ? "C"
                          : p.nota == 7
                          ? "D"
                          : "E"}
                      </td>
                      <td>{p.provimi && p.provimi.statusiINotes}</td>
                    </tr>
                  );
                })}
              <tr>
                <td>-</td>
                <td>-</td>
                <td>-</td>
                <td>
                  <strong>{parseFloat(totalKredi).toFixed(2)}</strong>
                </td>
                <td>-</td>
                <td>
                  <strong>{parseFloat(notaMesatare).toFixed(2)}</strong>
                </td>
                <td>-</td>
                <td>-</td>
              </tr>
            </tbody>
          </table>
        </>
      )}
    </div>
  );
};

export default TranskriptaNotave;
