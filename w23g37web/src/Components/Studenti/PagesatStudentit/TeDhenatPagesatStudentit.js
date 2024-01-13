import "./Styles/Fatura.css";
import axios from "axios";
import { useState, useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";

function TeDhenatPagesatStudentit(props) {
  const [perditeso, setPerditeso] = useState(Date.now());
  const [teDhenatBiznesit, setTeDhenatBiznesit] = useState([]);

  const [produktet, setProduktet] = useState([]);
  const [teDhenatFat, setteDhenatFat] = useState([]);

  const [notat, setNotat] = useState([]);

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const vendosDetajetVertetimitStudentor = async () => {
      try {
        const notat = await axios.get(
          `https://localhost:7251/api/Studentet/ShfaqPagesatStudentit?studentiID=${props.id}`,
          authentikimi
        );
        setNotat(notat.data);
        console.log(notat.data.pagesatList);
      } catch (err) {
        console.log(err);
      }
    };

    vendosDetajetVertetimitStudentor();
  }, [props.id, perditeso]);

  useEffect(() => {
    const vendosTeDhenatBiznesit = async () => {
      try {
        const teDhenat = await axios.get(
          `https://localhost:7251/api/Studentet/ShfaqInfoPagesatStudentit?studentiID=${props.id}`,
          authentikimi
        );
        setTeDhenatBiznesit(teDhenat.data);
      } catch (err) {
        console.log(err);
      }
    };

    vendosTeDhenatBiznesit();
  }, [props.faturaID]);

  return (
    <>
      <div className="tabelaETeDhenaveProduktit">
        <table>
          <tr style={{ fontSize: "15px" }}>
            <th>Nr.</th>
            <th>Urdher.</th>
            <th>Pershkrimi</th>
            <th>Viti</th>
            <th>Data</th>
            <th>Faturimi</th>
            <th>Pagesa</th>
            <th>Mbetja</th>
          </tr>
          {notat
            .slice(props.ProduktiPare, props.ProduktiFundit)
            .map((p, index) => {
              return (
                <tr style={{ fontSize: "15px" }} key={p.notaStudentiID}>
                  <td style={{ textAlign: "right" }}>
                    {index + 1 + props.ProduktiPare}
                  </td>
                  <td style={{ textAlign: "left" }}>{p.urdher}</td>
                  <td style={{ textAlign: "left" }}>{p.pershkrimi}</td>
                  <td>{p.viti}</td>
                  <td>
                    {new Date(p.data).toLocaleDateString("en-GB", {
                      dateStyle: "short",
                    })}
                  </td>
                  <td>{parseFloat(p.faturimi).toFixed(2)}</td>
                  <td>{parseFloat(p.pagesa).toFixed(2)}</td>
                  <td>{parseFloat(p.mbetja).toFixed(2)}</td>
                </tr>
              );
            })}
          {!props.LargoTotalin && (
            <tr style={{ fontSize: "15px" }}>
              <th>-</th>
              <th>-</th>
              <th>-</th>
              <th colSpan={2}>
                <strong>Gjithsej Studime</strong>
              </th>
              <th>
                <strong>
                  {parseFloat(
                    teDhenatBiznesit && teDhenatBiznesit.faturimi
                  ).toFixed(2)}
                </strong>
              </th>
              <th>
                <strong>
                  {parseFloat(
                    teDhenatBiznesit && teDhenatBiznesit.pagesat
                  ).toFixed(2)}
                </strong>
              </th>
              <th>
                <strong>
                  {parseFloat(
                    teDhenatBiznesit &&
                      teDhenatBiznesit.pagesat - teDhenatBiznesit.faturimi
                  ).toFixed(2)}
                </strong>
              </th>
            </tr>
          )}
        </table>
      </div>
    </>
  );
}

export default TeDhenatPagesatStudentit;
