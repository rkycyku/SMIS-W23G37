import axios from "axios";
import { useState, useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import jsPDF from "jspdf";
import html2canvas from "html2canvas";
import { Table } from "react-bootstrap";

function VertetimiStudentor(props) {
  const [perditeso, setPerditeso] = useState("");
  const [teDhenat, setTeDhenat] = useState([]);
  const [pregaditDokumentin, setPregaditDokumentin] = useState(false);

  const [vitiAkademikRegjistrim, setVitiAkademikRegjistrim] = useState("");

  const getID = localStorage.getItem("id");
  const navigate = useNavigate();

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    if (getID) {
      const vendosDetajetVertetimitStudentor = async () => {
        try {
          const detajetVertetimitStudentor = await axios.get(
            `https://localhost:7251/api/Administrata/ShfaqDetajetVertetimiStudentore?userID=${props.id}`,
            authentikimi
          );
          setTeDhenat(detajetVertetimitStudentor.data);

          if (detajetVertetimitStudentor.status == "200") {
            setPregaditDokumentin(true);
          }
        } catch (err) {
          console.log(err);
        }
      };

      const vendosVitinAkademik = async () => {
        const vitiAkademikRegjistrim = await axios.get(
          `https://localhost:7251/api/Administrata/GjeneroVitinAkademik`,
          authentikimi
        );

        setVitiAkademikRegjistrim(vitiAkademikRegjistrim.data);
      };

      vendosVitinAkademik();
      vendosDetajetVertetimitStudentor();
    } else {
      navigate("/login");
    }
  }, [perditeso]);

  useEffect(() => {
    if (pregaditDokumentin === true) {
      RuajDetajetRegjistrimit();
    }
  }, [pregaditDokumentin]);

  function RuajDetajetRegjistrimit() {
    const teDhenatAplikimitRef = document.querySelector(".teDhenatAplikimit");
    const studenti =
      ((teDhenat && teDhenat.emri) || "") +
      " " +
      ((teDhenat && teDhenat.mbiemri) || "") +
      " - " +
      (
        (teDhenat &&
          teDhenat.teDhenatRegjistrimitStudentit &&
          teDhenat.teDhenatRegjistrimitStudentit.idStudenti) ||
        ""
      ).toString();

    html2canvas(teDhenatAplikimitRef, { useCORS: true })
      .then((invoiceCanvas) => {
        var pdf = new jsPDF("", "pt", "a4");
        var imgData = invoiceCanvas.toDataURL("image/jpeg", 1.0);
        var imgWidth = 595.28;
        var imgHeight = (invoiceCanvas.height * imgWidth) / invoiceCanvas.width;

        pdf.addImage(imgData, "JPEG", 0, 0, imgWidth, imgHeight);
        pdf.save("Vertetimi Studentore per " + studenti + ".pdf");
        props.setMbyllTeDhenat();
      })
      .catch((error) => {
        console.error(error);
      });
  }

  return (
    <>
      <div className="teDhenatAplikimit">
        <div className="teDhenatAplikimitHeader">
          <img
            src={`${process.env.PUBLIC_URL}/Ubtlogo.png`}
            style={{ width: "100px", height: "auto", marginTop: "0.5em" }}
          />
          <h4>Universiteti për Biznes dhe Teknologji</h4>
          <h4>Administrata</h4>
          <h4>Vërtetimi Studentor</h4>
        </div>
        <hr />
        <div className="teDhenatAplikimitFooter">
          <p>
            Numri i Protokollit: <strong>____/____-____</strong>
          </p>
          <p style={{"margin-bottom": "5em"}}>
            Datë:{" "}
            <strong>
              {new Date(Date.now()).toLocaleDateString("en-GB", {
                dateStyle: "short",
              })}
            </strong>
          </p>
          <p>
            Në bazë të kërkesës së studentit dhe në bazë të nenit 53, të
            Statutit të BPrAL Kolegji UBT, Programi për{" "}
            <strong>
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.departamentet &&
                teDhenat.teDhenatRegjistrimitStudentit.departamentet
                  .emriDepartamentit}
            </strong>{" "}
            lëshon këtë: <h1 style={{"margin": "1em"}}>Vërtetim për statusin e Studentit</h1>
          </p>

          <p>
            Kolegji <strong>UBT</strong> vërteton se studenti{" "}
            <strong>
              {teDhenat && teDhenat.emri} {teDhenat && teDhenat.mbiemri}
            </strong>
            ,{" "}
            {teDhenat &&
            teDhenat.teDhenatPerdoruesit &&
            teDhenat.teDhenatPerdoruesit.gjinia == "M"
              ? "i"
              : "e"}{" "}
            Lindur më{" "}
            <strong>
              {new Date(
                teDhenat &&
                  teDhenat.teDhenatPerdoruesit &&
                  teDhenat.teDhenatPerdoruesit.dataLindjes
              ).toLocaleDateString("en-GB", { dateStyle: "short" })}
            </strong>{" "}
            në{" "}
            <strong>
              {teDhenat &&
                teDhenat.teDhenatPerdoruesit &&
                teDhenat.teDhenatPerdoruesit.qyteti}
            </strong>
            , me Nr. Indeksit{" "}
            <strong>
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.idStudenti}
            </strong>
            , Dosja{" "}
            <strong>
              Nr.{" "}
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.idStudenti}
            </strong>
            ,{" "}
            {teDhenat &&
            teDhenat.teDhenatPerdoruesit &&
            teDhenat.teDhenatPerdoruesit.gjinia == "M"
              ? "i"
              : "e"}{" "}
            regjistruar për herë të parë në vitin akademik{" "}
            <strong>
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.vitiAkademikRegjistrim}
            </strong>
            , eshtë student{" "}
            
            <strong>{teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.llojiRegjistrimit}</strong> në vitin e{" "}
            <strong>
              {Math.ceil(
                teDhenat &&
                  teDhenat.paraqitjaSemestrit &&
                  teDhenat.paraqitjaSemestrit.semestri &&
                  teDhenat.paraqitjaSemestrit.semestri.nrSemestrit / 2
              )}
              .
            </strong>{" "}
            Për vitin akademik <strong>{vitiAkademikRegjistrim}</strong>{" "}
            {teDhenat &&
            teDhenat.teDhenatPerdoruesit &&
            teDhenat.teDhenatPerdoruesit.gjinia == "M"
              ? "studenti"
              : "studentja"} ka regjistruar semestrin e{" "}
            <strong>
              {teDhenat &&
                teDhenat.paraqitjaSemestrit &&
                teDhenat.paraqitjaSemestrit.semestri &&
                teDhenat.paraqitjaSemestrit.semestri.nrSemestrit}.
            </strong>
          </p>

          <p>
            Studimet në{" "}
            <strong>
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve &&
                teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve
                  .emriNivelitStudimeve}
              ,{" "}
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.departamentet &&
                teDhenat.teDhenatRegjistrimitStudentit.departamentet
                  .emriDepartamentit}
            </strong>
            , zgjasin{" "}
            <strong>
              {teDhenat &&
                teDhenat.teDhenatRegjistrimitStudentit &&
                teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve &&
                teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve
                  .semestrat &&
                teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve.semestrat
                  .length}
            </strong>{" "}
            semestra{" "}
            <strong>
              {Math.ceil(
                teDhenat &&
                  teDhenat.teDhenatRegjistrimitStudentit &&
                  teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve &&
                  teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve
                    .semestrat &&
                  teDhenat.teDhenatRegjistrimitStudentit.niveliStudimeve
                    .semestrat.length / 2
              )}
            </strong>{" "}
            vite.
          </p>
          <p>
            Sipas Statuti të Kolegjit UBT, studenti duhet t'i perfundojë
            studimet brenda periudhës së dyfishtë të studimeve të rregullta.
          </p>

          <p style={{"margin-top": "50em"}}><strong>Zyrtari Administrativ</strong></p>
          <p>______________________</p>
          <br />
          <p>______________________</p>

          <br />
          <p><strong>Menaxheri i Zyres</strong></p>
          <br />
          <p>______________________</p>
        </div>
      </div>
    </>
  );
}

export default VertetimiStudentor;
