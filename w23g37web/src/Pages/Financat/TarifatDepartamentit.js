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

const TarifatDepartamentit = () => {
  const [teDhenat, setTeDhenat] = useState([]);
  const [id, setId] = useState();
  const [perditeso, setPerditeso] = useState("");
  const [shfaqTeDhenat, setShfaqTeDhenat] = useState(false);
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
    const shfaqDepartamentet = async () => {
      try {
        setLoading(true);
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
          <DetajetTarifatDepartamentit setMbyllTeDhenat={() => {setShfaqTeDhenat(false); setPerditeso(Date.now())}} id={id} />
        )}
          {!shfaqTeDhenat && (
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
                {teDhenat &&
                  teDhenat.departamentet &&
                  teDhenat.departamentet.map((p) => {
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
                            onClick={() => handleShfaqTeDhenat(p.departamentiID)}>
                            <FontAwesomeIcon icon={faPenToSquare} />
                          </Button>
                        </td>
                      </tr>
                    );
                  })}
              </tbody>
            </table>
          )}
        </>
      )}
    </div>
  );
};

export default TarifatDepartamentit;
