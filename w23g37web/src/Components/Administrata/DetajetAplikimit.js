import axios from "axios";
import { useState, useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import jsPDF from "jspdf";
import html2canvas from "html2canvas";
import { Table } from "react-bootstrap";

function DetajetAplikimit(props) {
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
            `https://localhost:7251/api/Administrata/ShfaqAplikiminNgaID?id=${props.id}`,
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
      };

      vendosLlogariteBankare();
      vendosAplikimin();
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
      ((teDhenatAplikimit &&
        teDhenatAplikimit.aplikimetEReja &&
        teDhenatAplikimit.aplikimetEReja.emri) ||
        "") +
      " " +
      ((teDhenatAplikimit &&
        teDhenatAplikimit.aplikimetEReja &&
        teDhenatAplikimit.aplikimetEReja.emriPrindit) ||
        "") +
      " " +
      (
        (teDhenatAplikimit &&
          teDhenatAplikimit.aplikimetEReja &&
          teDhenatAplikimit.aplikimetEReja.mbiemri) ||
        ""
      ).toString();

    html2canvas(teDhenatAplikimitRef, { useCORS: true })
      .then((invoiceCanvas) => {
        var pdf = new jsPDF("", "pt", "a4");
        var imgData = invoiceCanvas.toDataURL("image/jpeg", 1.0);
        var imgWidth = 595.28;
        var imgHeight = (invoiceCanvas.height * imgWidth) / invoiceCanvas.width;

        pdf.addImage(imgData, "JPEG", 0, 0, imgWidth, imgHeight);
        pdf.save("Te Dhenat E Aplikimit per " + studenti + ".pdf");
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

          <hr style={{ color: "black", width: "200vh" }} />
          <h4>Te Dhenat e Aplikimit</h4>
        </div>
        <Table striped bordered hover className="mt-3">
          <thead>
            <tr>
              <th>Studenti</th>
              <td>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.emri}{" "}
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.emriPrindit}{" "}
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.mbiemri}
              </td>
              <th>Numri Personal</th>
              <td>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.nrPersonal}
              </td>
            </tr>
            <tr>
              <th>Data Aplikimit</th>
              <td>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  new Date(
                    teDhenatAplikimit.aplikimetEReja.dataRegjistrimit
                  ).toLocaleString("en-GB", {
                    dateStyle: "short",
                    timeStyle: "medium",
                  })}
              </td>
              <th>Gjinia</th>
              <td>
                {teDhenatAplikimit &&
                teDhenatAplikimit.aplikimetEReja &&
                teDhenatAplikimit.aplikimetEReja.gjinia &&
                teDhenatAplikimit.aplikimetEReja.gjinia == "M"
                  ? "Mashkull"
                  : "Femer"}
              </td>
            </tr>
            <tr>
              <th>Data Lindjes</th>
              <td>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  new Date(
                    teDhenatAplikimit.aplikimetEReja.dataLindjes
                  ).toLocaleDateString("en-GB", { dateStyle: "short" })}
              </td>
              <th>Kodi Financiar</th>
              <td>
                <strong>
                  {teDhenatAplikimit &&
                    teDhenatAplikimit.aplikimetEReja &&
                    teDhenatAplikimit.aplikimetEReja.kodiFinanciar}
                </strong>
              </td>
            </tr>
            <br></br>
            <tr>
              <th>Email Personal</th>
              <td>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.emailPersonal}
              </td>
              <th>Nr Kontaktit</th>
              <td>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.nrKontaktit}
              </td>
            </tr>
            <tr>
              <th>Adresa</th>
              <td>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.adresa}
                ,{" "}
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.qyteti}
                ,{" "}
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.zipKodi}{" "}
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.shteti}
              </td>
            </tr>
            <br></br>
            <tr>
              <th>Departamenti</th>
              <td>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.departamentet &&
                  teDhenatAplikimit.aplikimetEReja.departamentet
                    .emriDepartamentit}{" "}
                -{" "}
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.departamentet &&
                  teDhenatAplikimit.aplikimetEReja.departamentet
                    .shkurtesaDepartamentit}
              </td>
              <th>Niveli Studimeve</th>
              <td>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.niveliStudimeve &&
                  teDhenatAplikimit.aplikimetEReja.niveliStudimeve
                    .emriNivelitStudimeve}{" "}
                -{" "}
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.niveliStudimeve &&
                  teDhenatAplikimit.aplikimetEReja.niveliStudimeve
                    .shkurtesaEmritNivelitStudimeve}
              </td>
            </tr>
            <tr>
              <th>Lloji Regjistrimit</th>
              <td>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.llojiRegjistrimit}
              </td>
              <th>Lloji Kontrates</th>
              <td>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.llojiKontrates}{" "}
                Vjeqare
              </td>
            </tr>
            {teDhenatAplikimit &&
              teDhenatAplikimit.aplikimetEReja &&
              teDhenatAplikimit.aplikimetEReja.zbritja && (
                <tr>
                  <th>Zbritja</th>
                  <td>
                    {teDhenatAplikimit &&
                      teDhenatAplikimit.aplikimetEReja &&
                      teDhenatAplikimit.aplikimetEReja.zbritja &&
                      teDhenatAplikimit.aplikimetEReja.zbritja
                        .emriZbritjes}{" "}
                    - Zbritja:{" "}
                    {teDhenatAplikimit &&
                      teDhenatAplikimit.aplikimetEReja &&
                      teDhenatAplikimit.aplikimetEReja.zbritja &&
                      parseFloat(
                        teDhenatAplikimit.aplikimetEReja.zbritja.zbritja
                      ).toFixed(2)}{" "}
                    % - Kohezgjatja:{" "}
                    {teDhenatAplikimit &&
                      teDhenatAplikimit.aplikimetEReja &&
                      teDhenatAplikimit.aplikimetEReja.zbritja &&
                      teDhenatAplikimit.aplikimetEReja.zbritja.llojiZbritjes}
                  </td>
                </tr>
              )}
          </thead>
        </Table>
        <hr />
        <div className="teDhenatAplikimitFooter">
          <strong>
            <i>Pagesa duhet te behet ne nje nga bankat e cekura me poshte!</i>
          </strong>

          <p>
            <i>
              Pagesa juaj fillestare duhet te behet per 3 muaj, Shenoni kodin
              financiar gjete pageses!
            </i>
            <p>
              Kodi Juaj Financiar eshte:{" "}
              <strong>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.aplikimetEReja &&
                  teDhenatAplikimit.aplikimetEReja.kodiFinanciar}
              </strong>
            </p>
          </p>
          {teDhenatAplikimit &&
          teDhenatAplikimit.aplikimetEReja &&
          teDhenatAplikimit.aplikimetEReja.zbritja ? (
            <>
              <p>
                Totali Vjetor Pa Zbritje:{" "}
                <strong>
                  {teDhenatAplikimit &&
                    teDhenatAplikimit.tarifaDepartamenti &&
                    parseFloat(
                      teDhenatAplikimit.tarifaDepartamenti.tarifaVjetore
                    ).toFixed(2)}{" "}
                  €
                </strong>
              </p>
              <p>
                Zbritja:{" "}
                <strong>
                  {teDhenatAplikimit &&
                    teDhenatAplikimit.tarifaDepartamenti &&
                    parseFloat(
                      (teDhenatAplikimit.tarifaDepartamenti.tarifaVjetore *
                        (teDhenatAplikimit &&
                          teDhenatAplikimit.aplikimetEReja &&
                          teDhenatAplikimit.aplikimetEReja.zbritja &&
                          teDhenatAplikimit.aplikimetEReja.zbritja.zbritja)) /
                        100
                    ).toFixed(2)}{" "}
                  €
                </strong>
              </p>
              <p>
                Totali Vjetor Me Zbritje:{" "}
                <strong>
                  {teDhenatAplikimit &&
                    teDhenatAplikimit.tarifaDepartamenti &&
                    parseFloat(
                      teDhenatAplikimit.tarifaDepartamenti.tarifaVjetore -
                        (teDhenatAplikimit.tarifaDepartamenti.tarifaVjetore *
                          (teDhenatAplikimit &&
                            teDhenatAplikimit.aplikimetEReja &&
                            teDhenatAplikimit.aplikimetEReja.zbritja &&
                            teDhenatAplikimit.aplikimetEReja.zbritja.zbritja)) /
                          100
                    ).toFixed(2)}{" "}
                  €
                </strong>
              </p>
            </>
          ) : (
            <p>
              Totali Vjetor:{" "}
              <strong>
                {teDhenatAplikimit &&
                  teDhenatAplikimit.tarifaDepartamenti &&
                  parseFloat(
                    teDhenatAplikimit.tarifaDepartamenti.tarifaVjetore
                  ).toFixed(2)}{" "}
                €
              </strong>
            </p>
          )}

          <h5>
            Pagesa juaj qe duhet te behet per 3 keste te menjehershem eshte:{" "}
            <strong>
              {teDhenatAplikimit &&
                teDhenatAplikimit.tarifaDepartamenti &&
                parseFloat(
                  ((teDhenatAplikimit.tarifaDepartamenti.tarifaVjetore -
                    (teDhenatAplikimit.tarifaDepartamenti.tarifaVjetore *
                      (teDhenatAplikimit &&
                        teDhenatAplikimit.aplikimetEReja &&
                        teDhenatAplikimit.aplikimetEReja.zbritja &&
                        teDhenatAplikimit.aplikimetEReja.zbritja.zbritja)) /
                      100) /
                    12) *
                    3
                ).toFixed(2)}{" "}
              €
            </strong>
          </h5>
          <h6>
            <i>
              Ne rast se behet pagesa ne teresi ju fitoni
              <strong> 5% </strong>zbritje ekstra, ne ate rast Totali Juaj per
              Pagese do te jete:{" "}
              {teDhenatAplikimit &&
              teDhenatAplikimit.aplikimetEReja &&
              teDhenatAplikimit.aplikimetEReja.zbritja ? (
                <strong>
                  {teDhenatAplikimit &&
                    teDhenatAplikimit.tarifaDepartamenti &&
                    parseFloat(
                      teDhenatAplikimit.tarifaDepartamenti.tarifaVjetore -
                        (teDhenatAplikimit.tarifaDepartamenti.tarifaVjetore *
                          (teDhenatAplikimit &&
                            teDhenatAplikimit.aplikimetEReja &&
                            teDhenatAplikimit.aplikimetEReja.zbritja &&
                            teDhenatAplikimit.aplikimetEReja.zbritja.zbritja +
                              5)) /
                          100
                    ).toFixed(2)}{" "}
                  €
                </strong>
              ) : (
                <strong>
                  {teDhenatAplikimit &&
                    teDhenatAplikimit.tarifaDepartamenti &&
                    parseFloat(
                      teDhenatAplikimit.tarifaDepartamenti.tarifaVjetore -
                        (teDhenatAplikimit.tarifaDepartamenti.tarifaVjetore *
                          5) /
                          100
                    ).toFixed(2)}{" "}
                  €
                </strong>
              )}
            </i>
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
    </>
  );
}

export default DetajetAplikimit;
