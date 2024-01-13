import "./Styles/Fatura.css";
import axios from "axios";
import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import jsPDF from "jspdf";
import html2canvas from "html2canvas";
import DetajePagesatStudentit from "./DetajePagesatStudentit";
import { MDBBtn } from "mdb-react-ui-kit";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowLeft, faDownload } from "@fortawesome/free-solid-svg-icons";

function PagesatStudentit(props) {
  const [perditeso, setPerditeso] = useState("");
  const [vendosFature, setVendosFature] = useState(false);
  const [teDhenat, setTeDhenat] = useState([]);

  const [meShumeSe30, setMeShumeSe30] = useState(false);
  const [meShumeSe65, setMeShumeSe65] = useState(false);

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
            `https://localhost:7251/api/Studentet/ShfaqPagesatStudentit?studentiID=${props.id}`,
            authentikimi
          );
          const pagesat = await axios.get(
            `https://localhost:7251/api/Studentet/ShfaqInfoPagesatStudentit?studentiID=${props.id}`,
            authentikimi
          );
          setNotat(notat.data);
          setTeDhenat(pagesat.data);

          if (notat.data.length > 30) {
            setMeShumeSe30(true);
            setNrFaqeve(2);
          }

          if (notat.data.length > 65) {
            setMeShumeSe65(true);
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
    const meShumeSe30Ref = document.querySelector(".meShumeSe30");
    const meShumeSe65Ref = document.querySelector(".meShumeSe65");

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

        if (!meShumeSe30) {
          ruajFaturen(pdf);
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

              if (!meShumeSe65) {
                ruajFaturen(pdf);
              }
            })
            .catch((error) => {
              ruajFaturen(pdf);
            });
        }
        if (meShumeSe65) {
          html2canvas(meShumeSe65Ref, { useCORS: true })
            .then((meShumeSe65Canvas) => {
              var meShumeSe65Width = meShumeSe65Canvas.width;
              var meShumeSe65Height = meShumeSe65Canvas.height;
              var meShumeSe65PageHeight = (meShumeSe65Width / 592.28) * 841.89;
              var meShumeSe65LeftHeight = meShumeSe65Height;
              var meShumeSe65Position = 0;
              var meShumeSe65ImgWidth = 555.28;
              var meShumeSe65ImgHeight =
                (meShumeSe65ImgWidth / meShumeSe65Width) * meShumeSe65Height;
              var meShumeSe65PageData = meShumeSe65Canvas.toDataURL(
                "image/jpeg",
                1.0
              );

              if (meShumeSe65LeftHeight < meShumeSe65PageHeight) {
                pdf.addPage();
                pdf.addImage(
                  meShumeSe65PageData,
                  "JPEG",
                  20,
                  0,
                  meShumeSe65ImgWidth,
                  meShumeSe65ImgHeight
                );
              } else {
                while (meShumeSe65LeftHeight > 0) {
                  pdf.addPage();
                  pdf.addImage(
                    meShumeSe65PageData,
                    "JPEG",
                    20,
                    meShumeSe65Position,
                    meShumeSe65ImgWidth,
                    meShumeSe65ImgHeight
                  );
                  meShumeSe65LeftHeight -= meShumeSe65PageHeight;
                  meShumeSe65Position -= 841.89;
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
      ((teDhenat && teDhenat.studenti) || "") +
      " - " +
      ((teDhenat && teDhenat.kodiFinanciar) || "").toString();

    pdf.save("Kartela Analitike per " + studenti + ".pdf");
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
        <DetajePagesatStudentit
          id={props.id}
          ProduktiPare={0}
          ProduktiFundit={meShumeSe30 ? 30 : 30}
          LargoHeader={false}
          NrFaqes={1}
          NrFaqeve={nrFaqeve}
          LargoTotalin={meShumeSe30}
        />
      </div>
      {meShumeSe30 && (
        <div className="meShumeSe30">
          <DetajePagesatStudentit
            id={props.id}
            ProduktiPare={30}
            ProduktiFundit={meShumeSe65 ? 65 : 65}
            LargoHeader={meShumeSe30}
            NrFaqes={2}
            NrFaqeve={nrFaqeve}
            LargoTotalin={meShumeSe65}
          />
        </div>
      )}
      {meShumeSe65 && (
        <div className="meShumeSe65">
          <DetajePagesatStudentit
            id={props.id}
            ProduktiPare={65}
            ProduktiFundit={100}
            LargoHeader={meShumeSe65}
            NrFaqes={3}
            NrFaqeve={nrFaqeve}
            LargoTotalin={false}
          />
        </div>
      )}
    </>
  );
}

export default PagesatStudentit;
