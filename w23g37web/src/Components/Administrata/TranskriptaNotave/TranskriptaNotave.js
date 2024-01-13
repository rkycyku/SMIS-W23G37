import "./Styles/Fatura.css";
import axios from "axios";
import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import jsPDF from "jspdf";
import html2canvas from "html2canvas";
import DetajeTranskriptaNotave from "./DetajeTranskriptaNotave";
import { MDBBtn } from "mdb-react-ui-kit";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowLeft, faDownload } from "@fortawesome/free-solid-svg-icons";

function TranskriptaNotave(props) {
  const [perditeso, setPerditeso] = useState("");
  const [vendosFature, setVendosFature] = useState(false);
  const [teDhenat, setTeDhenat] = useState([]);

  const [meShumeSe15, setMeShumeSe15] = useState(false);
  const [meShumeSe30, setMeShumeSe30] = useState(false);

  const [nrFaqeve, setNrFaqeve] = useState(1);

  const [pregaditDokumentin, setPregaditDokumentin] = useState(false);

  const [notat, setNotat] = useState([]);

  const [kaAkses, setKaAkses] = useState(true);

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
          const notat = await axios.get(
            `https://localhost:7251/api/Studentet/ShfaqTranskriptenNotaveStudentit?studentiID=${props.id}`,
            authentikimi
          );
          setNotat(notat.data.notatStudentiList);
          setTeDhenat(notat.data.perdoruesi);

          if (notat.data.notatStudentiList.length > 15) {
            setMeShumeSe15(true);
            setNrFaqeve(2);
          }

          if (notat.data.notatStudentiList.length > 40) {
            setMeShumeSe30(true);
            setNrFaqeve(3);
          }

          setVendosFature(true);

          if (notat.status == "200") {
            setPregaditDokumentin(true);
          }
        } catch (err) {
          console.log(err);
        }
      };

      vendosDetajetVertetimitStudentor();
    } else {
      navigate("/login");
    }
  }, [perditeso]);

  useEffect(() => {
    if (getID) {
      const vendosTeDhenatUserit = async () => {
        try {
          const teDhenatUser = await axios.get(
            `https://localhost:7285/api/Perdoruesi/shfaqSipasID?idUserAspNet=${getID}`,
            authentikimi
          );
          setTeDhenat(teDhenatUser.data);
          if (!teDhenatUser.data.rolet.includes("Admin", "Menaxher")) {
            setKaAkses(false);
          }
          console.log(teDhenatUser.data);
        } catch (err) {
          console.log(err);
        }
      };

      vendosTeDhenatUserit();
    } else {
      navigate("/login");
    }
  }, [getID]);

  useEffect(() => {
    console.log(teDhenat);
    if (teDhenat) {
      if (!kaAkses) {
        navigate("/dashboard");
      } else {
        if (vendosFature === true) {
          // FaturaPerRuajtje();
        }
      }
    }
  }, [vendosFature]);

  function FaturaPerRuajtje() {
    const mePakSe25Ref = document.querySelector(".mePakSe25");
    const meShumeSe15Ref = document.querySelector(".meShumeSe15");
    const meShumeSe30Ref = document.querySelector(".meShumeSe30");

    html2canvas(mePakSe25Ref, { useCORS: true })
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

        if (!meShumeSe15) {
          ruajFaturen(pdf);
        }

        if (meShumeSe15) {
          html2canvas(meShumeSe15Ref, { useCORS: true })
            .then((meShumeSe15Canvas) => {
              var meShumeSe15Width = meShumeSe15Canvas.width;
              var meShumeSe15Height = meShumeSe15Canvas.height;
              var meShumeSe15PageHeight = (meShumeSe15Width / 592.28) * 841.89;
              var meShumeSe15LeftHeight = meShumeSe15Height;
              var meShumeSe15Position = 0;
              var meShumeSe15ImgWidth = 555.28;
              var meShumeSe15ImgHeight =
                (meShumeSe15ImgWidth / meShumeSe15Width) * meShumeSe15Height;
              var meShumeSe15PageData = meShumeSe15Canvas.toDataURL(
                "image/jpeg",
                1.0
              );

              if (meShumeSe15LeftHeight < meShumeSe15PageHeight) {
                pdf.addPage();
                pdf.addImage(
                  meShumeSe15PageData,
                  "JPEG",
                  20,
                  0,
                  meShumeSe15ImgWidth,
                  meShumeSe15ImgHeight
                );
              } else {
                while (meShumeSe15LeftHeight > 0) {
                  pdf.addPage();
                  pdf.addImage(
                    meShumeSe15PageData,
                    "JPEG",
                    20,
                    meShumeSe15Position,
                    meShumeSe15ImgWidth,
                    meShumeSe15ImgHeight
                  );
                  meShumeSe15LeftHeight -= meShumeSe15PageHeight;
                  meShumeSe15Position -= 841.89;
                }
              }

              if (!meShumeSe30) {
                ruajFaturen(pdf);
              }
            })
            .catch((error) => {
              ruajFaturen(pdf);
            });
        }
        if (meShumeSe30) {
          html2canvas(meShumeSe30Ref, { useCORS: true })
            .then((meShumeSe30Canvas) => {
              var meShumeSe30Width = meShumeSe30Canvas.width;
              var meShumeSe30Height = meShumeSe30Canvas.height;
              var meShumeSe30PageHeight = (meShumeSe30Width / 592.28) * 841.89;
              var meShumeSe30LeftHeight = meShumeSe30Height;
              var meShumeSe30Position = 0;
              var meShumeSe30ImgWidth = 555.28;
              var meShumeSe30ImgHeight =
                (meShumeSe30ImgWidth / meShumeSe30Width) * meShumeSe30Height;
              var meShumeSe30PageData = meShumeSe30Canvas.toDataURL(
                "image/jpeg",
                1.0
              );

              if (meShumeSe30LeftHeight < meShumeSe30PageHeight) {
                pdf.addPage();
                pdf.addImage(
                  meShumeSe30PageData,
                  "JPEG",
                  20,
                  0,
                  meShumeSe30ImgWidth,
                  meShumeSe30ImgHeight
                );
              } else {
                while (meShumeSe30LeftHeight > 0) {
                  pdf.addPage();
                  pdf.addImage(
                    meShumeSe30PageData,
                    "JPEG",
                    20,
                    meShumeSe30Position,
                    meShumeSe30ImgWidth,
                    meShumeSe30ImgHeight
                  );
                  meShumeSe30LeftHeight -= meShumeSe30PageHeight;
                  meShumeSe30Position -= 841.89;
                }
              }
              ruajFaturen(pdf);
            })
            .catch((error) => {
              ruajFaturen(pdf);
            });
        }
      })
      .catch((error) => {
        console.error(error);
      });
  }

  function ruajFaturen(pdf) {
    const studenti =
      ((teDhenat && teDhenat.emri) || "") +
      " " +
      ((teDhenat &&
        teDhenat.teDhenatPerdoruesit &&
        teDhenat.teDhenatPerdoruesit.emriPrindit) ||
        "") +
      " " +
      ((teDhenat && teDhenat.mbiemri) || "").toString();

    pdf.save("Transkripta e Notes per " + studenti + ".pdf");
    props.setMbyllTeDhenat();
  }

  return (
    <>
      <h1 className="title">
        <MDBBtn className="mb-3 Butoni" onClick={() => FaturaPerRuajtje()}>
          Ruaj <FontAwesomeIcon icon={faDownload} />
        </MDBBtn>
        <MDBBtn
          className="mb-3 Butoni"
          onClick={() => props.setMbyllTeDhenat()}>
          <FontAwesomeIcon icon={faArrowLeft} /> Mbyll
        </MDBBtn>
      </h1>
      <div className="mePakSe25">
        <DetajeTranskriptaNotave
          id={props.id}
          ProduktiPare={0}
          ProduktiFundit={meShumeSe15 ? 15 : 20}
          LargoHeader={false}
          NrFaqes={1}
          NrFaqeve={nrFaqeve}
        />
      </div>
      {meShumeSe15 && (
        <div className="meShumeSe15">
          <DetajeTranskriptaNotave
            id={props.id}
            ProduktiPare={15}
            ProduktiFundit={meShumeSe30 ? 30 : 40}
            LargoHeader={meShumeSe15}
            NrFaqes={2}
            NrFaqeve={nrFaqeve}
          />
        </div>
      )}
      {meShumeSe30 && (
        <div className="meShumeSe30">
          <DetajeTranskriptaNotave
            id={props.id}
            ProduktiPare={50}
            ProduktiFundit={65}
            LargoHeader={meShumeSe30}
            NrFaqes={3}
            NrFaqeve={nrFaqeve}
          />
        </div>
      )}
    </>
  );
}

export default TranskriptaNotave;
