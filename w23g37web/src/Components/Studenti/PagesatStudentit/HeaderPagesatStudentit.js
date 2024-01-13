import "./Styles/Fatura.css";
import axios from "axios";
import { useState, useEffect } from "react";
import { Table } from "react-bootstrap";

function HeaderPagesatStudentit(props) {
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
          `https://localhost:7251/api/Studentet/ShfaqInfoPagesatStudentit?studentiID=${props.id}`,
          authentikimi
        );
        setTeDhenat(notat.data);
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
          style={{ width: "70px", height: "auto", marginTop: "0.5em" }}
        />
        <h4>Universiteti për Biznes dhe Teknologji</h4>
        <h5>Kartela Analitike</h5>
      </div>

      <Table striped bordered hover className="mt-3">
        <thead>
          <tr style={{ fontSize: "15px" }}>
            <th>Studenti:</th>
            <td>
              <strong>{teDhenat && teDhenat.studenti}</strong>
            </td>
            <th>Kodi Financiar:</th>
            <td>
              <strong>{teDhenat && teDhenat.kodiFinanciar}</strong>
            </td>
          </tr>
          <tr style={{ fontSize: "15px" }}>
            <th>Drejtimi:</th>
            <td>
              <strong>{teDhenat && teDhenat.drejtimi}</strong>
            </td>
            <th>Koha:</th>
            <td>
              <strong>
                {new Date(Date.now()).toLocaleString("en-GB", {
                  dateStyle: "short",
                  timeStyle: "medium",
                })}
              </strong>
            </td>
          </tr>
        </thead>
      </Table>
    </>
  );
}

export default HeaderPagesatStudentit;
