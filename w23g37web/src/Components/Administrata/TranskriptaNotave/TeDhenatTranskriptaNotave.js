import "./Styles/Fatura.css";
import axios from "axios";
import { useState, useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";

function TeDhenatTranskriptaNotave(props) {
  const [perditeso, setPerditeso] = useState(Date.now());
  const [teDhenatBiznesit, setTeDhenatBiznesit] = useState([]);

  const [produktet, setProduktet] = useState([]);
  const [teDhenatFat, setteDhenatFat] = useState([]);

  const dataPorosise = new Date(
    teDhenatFat &&
      teDhenatFat.regjistrimet &&
      teDhenatFat.regjistrimet.dataRegjistrimit
  );
  const dita = dataPorosise.getDate().toString().padStart(2, "0");
  const muaji = (dataPorosise.getMonth() + 1).toString().padStart(2, "0");
  const viti = dataPorosise.getFullYear().toString().slice(-2);

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
          `https://localhost:7251/api/Studentet/ShfaqTranskriptenNotaveStudentit?studentiID=${props.id}`,
          authentikimi
        );
        setNotat(notat.data.notatStudentiList);
        console.log(notat.data.notatStudentiList);
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
          "https://localhost:7285/api/TeDhenatBiznesit/ShfaqTeDhenat",
          authentikimi
        );
        setTeDhenatBiznesit(teDhenat.data);
      } catch (err) {
        console.log(err);
      }
    };

    vendosTeDhenatBiznesit();
  }, [props.faturaID]);

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
    <>
      <div className="tabelaETeDhenaveProduktit">
        <table>
          <tr style={{ fontSize: "15px" }}>
            <th>Nr. Rendore</th>
            <th>Kodi i Lendes mesimore</th>
            <th>Emertimi i Lendes Mesimore</th>
            <th>Kohezgjatja e Kursit</th>
            <th>Nota</th>
            <th>ECTS Nota</th>
            <th>ECTS Kredite</th>
          </tr>
          {notat
            .slice(props.ProduktiPare, props.ProduktiFundit)
            .map((p, index) => {
              return (
                <tr style={{ fontSize: "15px" }} key={p.notaStudentiID}>
                  <td style={{ textAlign: "right" }}>
                    {index + 1 + props.ProduktiPare}
                  </td>
                  <td style={{ textAlign: "left" }}>
                    {p.lenda && p.lenda.kodiLendes}
                  </td>
                  <td style={{ textAlign: "left" }}>
                    {p.lenda && p.lenda.emriLendes}
                  </td>
                  <td>1 Semester</td>
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

                  <td>{p.lenda && p.lenda.kreditELendes}</td>
                </tr>
              );
            })}
          {props.LargoMesatarenKredit && (
            <tr style={{ fontSize: "15px" }}>
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
          )}
        </table>
      </div>
    </>
  );
}

export default TeDhenatTranskriptaNotave;
