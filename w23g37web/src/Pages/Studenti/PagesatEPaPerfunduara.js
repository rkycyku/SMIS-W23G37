import axios from "axios";
import { useState, useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import jsPDF from "jspdf";
import html2canvas from "html2canvas";
import { Table } from "react-bootstrap";
import { MDBBtn } from "mdb-react-ui-kit";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faDownload, faArrowLeft } from "@fortawesome/free-solid-svg-icons";
import { Helmet } from "react-helmet";
import "../Styles/ProductTables.css";

function PagesatEPaPerfunduara(props) {
  const [perditeso, setPerditeso] = useState("");
  const [teDhenatAplikimit, setTeDhenatAplikimit] = useState([]);
  const [pregaditDokumentin, setPregaditDokumentin] = useState(false);
  const [llogariteBankare, setLlogariteBankare] = useState([]);

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
      const vendosAplikimin = async () => {
        try {
          const teDhenatAplikimit = await axios.get(
            `https://localhost:7251/api/Studentet/ShfaqInfoPagesatStudentit?studentiID=${getID}`,
            authentikimi
          );
          setTeDhenatAplikimit(teDhenatAplikimit.data);
          console.log(teDhenatAplikimit.data);

          if (teDhenatAplikimit.status == "200") {
            setPregaditDokumentin(true);
          }
        } catch (err) {
          console.log(err);
        }
      };

      const vendosLlogariteBankare = async () => {
        const bankat = await axios.get(
          "https://localhost:7251/api/Administrata/ShfaqLlogariteBankare",
          authentikimi
        );

        setLlogariteBankare(bankat.data);

        const rolet = await axios.get(
          `https://localhost:7251/api/Perdoruesi/shfaqSipasID?idUserAspNet=${getID}`,
          authentikimi
        );
        if (!rolet.data.rolet.includes("Student")) {
          navigate("/");
        }
      };

      vendosLlogariteBankare();
      vendosAplikimin();
    } else {
      navigate("/login");
    }
  }, [perditeso]);

  function FaturaPerRuajtje() {
    const teDhenatAplikimit = document.querySelector(".teDhenatAplikimit");
    html2canvas(teDhenatAplikimit, { useCORS: true })
      .then((invoiceCanvas) => {
        var contentWidth = invoiceCanvas.width;
        var contentHeight = invoiceCanvas.height;
        var pageHeight = (contentWidth / 592.28) * 841.89;
        var leftHeight = contentHeight;
        var position = 0;
        var imgWidth = 555.28;
        var imgHeight = (imgWidth / contentWidth) * contentHeight;
        var invoicePageData = invoiceCanvas.toDataURL("image/jpeg", 1.0);
        var pdf = new jsPDF("", "pt", "a4");

        if (leftHeight < pageHeight) {
          pdf.addImage(invoicePageData, "JPEG", 20, 0, imgWidth, imgHeight);
        } else {
          while (leftHeight > 0) {
            pdf.addImage(
              invoicePageData,
              "JPEG",
              20,
              position,
              imgWidth,
              imgHeight
            );
            leftHeight -= pageHeight;
            position -= 841.89;
          }
        }

        ruajFaturen(pdf);
      })
      .catch((error) => {
        console.error(error);
      });
  }

  function ruajFaturen(pdf) {
    const studenti =
      ((teDhenatAplikimit && teDhenatAplikimit.studenti) || "") +
      " - " +
      ((teDhenatAplikimit && teDhenatAplikimit.kodiFinanciar) || "").toString();

    pdf.save("Informat e Pageses se Borxhit per " + studenti + ".pdf");
    props.setMbyllTeDhenat();
  }

  return (
    <>
      <div className="containerDashboardP">
        <Helmet>
          <title>Pagesat E Pa Perfunduara | UBT SMIS</title>
        </Helmet>
        <h1 className="title">
          <MDBBtn className="mb-3 Butoni" onClick={() => FaturaPerRuajtje()}>
            Ruaj <FontAwesomeIcon icon={faDownload} />
          </MDBBtn>
        </h1>
        <div className="teDhenatAplikimit">
          <div className="teDhenatAplikimitHeader">
            <img
              src={`${process.env.PUBLIC_URL}/Ubtlogo.png`}
              style={{ width: "100px", height: "auto", marginTop: "0.5em" }}
            />
            <h4>Universiteti për Biznes dhe Teknologji</h4>
            <h4>Financa</h4>

            <hr style={{ color: "black", width: "200vh" }} />
            <h4>Te Dhenat e Pageses se Borgjit</h4>
          </div>
          <table className="tableBig">
            <thead>
              <tr>
                <th>Studenti</th>
                <td>
                  <strong>
                    {teDhenatAplikimit && teDhenatAplikimit.studenti}
                  </strong>
                </td>
                <th>ID Studenti</th>
                <td>{teDhenatAplikimit && teDhenatAplikimit.studentiIDFK}</td>
              </tr>
              <tr>
                <th>Kodi Financiar</th>
                <td>
                  <strong>
                    {teDhenatAplikimit && teDhenatAplikimit.kodiFinanciar}
                  </strong>
                </td>
                <th>Data</th>
                <td>
                  {new Date(Date.now()).toLocaleString("en-GB", {
                    dateStyle: "short",
                    timeStyle: "medium",
                  })}
                </td>
              </tr>
              <br></br>
              <tr>
                <th>Departamenti</th>
                <td>{teDhenatAplikimit && teDhenatAplikimit.drejtimi}</td>
                <th>Niveli Studimeve</th>
                <td>{teDhenatAplikimit && teDhenatAplikimit.niveli}</td>
              </tr>
              <tr>
                <th>Pagesat</th>
                <td>
                  {teDhenatAplikimit &&
                    parseFloat(teDhenatAplikimit.pagesat).toFixed(2)}{" "}
                  €
                </td>
                <th>Faturimet</th>
                <td>
                  {teDhenatAplikimit &&
                    parseFloat(teDhenatAplikimit.faturimi).toFixed(2)}{" "}
                  €
                </td>
              </tr>
              <tr>
                <th colSpan={3}>Gjithsej Mbetja</th>
                <td colSpan={1}>
                  <strong>
                    {teDhenatAplikimit &&
                      parseFloat(teDhenatAplikimit.mbetja).toFixed(2)}{" "}
                    €
                  </strong>
                </td>
              </tr>
            </thead>
          </table>
          <hr />
          <div className="teDhenatAplikimitFooter">
            <h5>
              Pagesa juaj qe duhet te behet e menjehershem eshte:{" "}
              <strong>
                {teDhenatAplikimit &&
                  parseFloat(teDhenatAplikimit.mbetja * -1).toFixed(2)}{" "}
                €
              </strong>
            </h5>
            <h6>
              Ne rast se nuk perfundohet pagesa ju akoma nuk do te keni qasje ne
              sistem apo ne paraqitje provimesh, semestrave etj.
            </h6>

            <p>
              <strong>
                Pagesa duhet te behet ne nje nga bankat e cekura me poshte!
              </strong>
            </p>

            <h6>
              Ne rast se keni perfunduar pagesen dhe sistemi nuk eshte
              aktivizuar ende ju lutem kontkatoni me Zyren e Financave ne:{" "}
              <a href="mailto:finance@ubt-uni.net">finance@ubt-uni.net</a>
            </h6>

            <Table striped bordered hover className="mt-3">
              <thead>
                <tr>
                  <th colSpan={2}>Llogarite Bankare</th>
                </tr>
                <tr>
                  <th>Banka</th>
                  <th>Numri Xhirollogarise</th>
                </tr>
              </thead>
              <tbody>
                {llogariteBankare &&
                  llogariteBankare.map((bankat) => {
                    return (
                      <tr>
                        <td>
                          {bankat.emriBankes} - {bankat.kodiBankes}
                        </td>
                        <td>{bankat.numriLlogaris}</td>
                      </tr>
                    );
                  })}
              </tbody>
            </Table>
          </div>
        </div>
      </div>
    </>
  );
}

export default PagesatEPaPerfunduara;
