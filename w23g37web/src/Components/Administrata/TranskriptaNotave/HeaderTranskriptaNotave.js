import "./Styles/Fatura.css";
import axios from "axios";
import { useState, useEffect } from "react";
import { Table } from "react-bootstrap";

function HeaderFatura(props) {
  const [teDhenatBiznesit, setTeDhenatBiznesit] = useState([]);

  const [teDhenatFat, setteDhenatFat] = useState([]);
  const [teDhenat, setTeDhenat] = useState([]);
  const getID = localStorage.getItem("id");

  const [perditeso, setPerditeso] = useState(Date.now());

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const vendosFature = async () => {
      try {
        const notat = await axios.get(
          `https://localhost:7251/api/Studentet/ShfaqTranskriptenNotaveStudentit?studentiID=${props.id}`,
          authentikimi
        );
        setTeDhenat(notat.data.perdoruesi);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    vendosFature();
  }, [props.id, perditeso]);

  return (
    <>
      <div className="teDhenatAplikimitHeader">
        <img
          src={`${process.env.PUBLIC_URL}/Ubtlogo.png`}
          style={{ width: "50px", height: "auto", marginTop: "0.5em" }}
        />
        <h5>Universiteti për Biznes dhe Teknologji</h5>
        <h5>
          Programi per{" "}
          {teDhenat &&
            teDhenat.teDhenatRegjistrimitStudentit &&
            teDhenat.teDhenatRegjistrimitStudentit.departamentet &&
            teDhenat.teDhenatRegjistrimitStudentit.departamentet
              .emriDepartamentit}
        </h5>
      </div>

      <p style={{ fontSize: "15px" }}>
        Numri i Protokollit: <strong>____/____-____</strong>
      </p>
      <p style={{ fontSize: "15px" }}>
        Datë:{" "}
        <strong>
          {new Date(Date.now()).toLocaleDateString("en-GB", {
            dateStyle: "short",
          })}
        </strong>
      </p>
      <Table striped bordered hover className="mt-3">
        <thead>
          <tr style={{ fontSize: "15px" }}>
            <th>Programi</th>
            <td colSpan={3}>
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.departamentet &&
                teDhenat.teDhenatRegjistrimitStudentit.departamentet
                  .emriDepartamentit}
            </td>
          </tr>
          <tr style={{ fontSize: "15px" }}>
            <th>Niveli i Studimeve</th>
            <td colSpan={3}>
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve &&
                teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve
                  .emriNivelitStudimeve}{" "}
              -{" "}
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve &&
                teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve
                  .shkurtesaEmritNivelitStudimeve}
            </td>
          </tr>
          <tr style={{ fontSize: "15px" }}>
            <th>Tel / Fax</th>
            <td>
              <strong>+383 38 541 400 / +383 38 542 138</strong>
            </td>
            <th>Email Adresa</th>
            <td>
              <strong>adminstud@ubt-uni.net</strong>
            </td>
          </tr>
          <br></br>
          <tr style={{ fontSize: "15px" }}>
            <th>Mbiemri</th>
            <td>{teDhenat && teDhenat.mbiemri}</td>
            <th>Emri</th>
            <td>{teDhenat && teDhenat.emri}</td>
          </tr>
          <tr style={{ fontSize: "15px" }}>
            <th>Data e Lindjes</th>
            <td>
              {teDhenat &&
                teDhenat.teDhenatPerdoruesit &&
                new Date(
                  teDhenat.teDhenatPerdoruesit.dataLindjes
                ).toLocaleDateString("en-GB", { dateStyle: "short" })}
            </td>
            <th>Vendi i Lindjes</th>
            <td>
              {teDhenat &&
                teDhenat.teDhenatPerdoruesit &&
                teDhenat.teDhenatPerdoruesit.qyteti}
            </td>
          </tr>
          <tr style={{ fontSize: "15px" }}>
            <th>Data e Regjistrimit</th>
            <td>
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                new Date(
                  teDhenat.teDhenatRegjistrimitStudentit.dataRegjistrimit
                ).toLocaleDateString("en-GB", { dateStyle: "short" })}
            </td>
            <th>Numri i Studentit</th>
            <td>
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.idStudenti}
            </td>
          </tr>
          <tr style={{ fontSize: "15px" }}>
            <th>Menyra e Studimit</th>
            <td>
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.llojiRegjistrimit}
            </td>
            <th>Viti Akademik</th>
            <td>
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.vitiAkademikRegjistrim}
            </td>
          </tr>
        </thead>
      </Table>
    </>
  );
}

export default HeaderFatura;
