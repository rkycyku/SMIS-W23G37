import React from "react";
import { Helmet } from "react-helmet";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/esm/Button";
import "../Styles/SignUp.css";
import { useState, useEffect } from "react";
import axios from "axios";
import Col from "react-bootstrap/Col";
import { Row } from "react-bootstrap";
import Mesazhi from "../../Components/TeTjera/layout/Mesazhi";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useNavigate } from "react-router-dom";

const KrijoAplikiminERi = () => {
  const [emri, setEmri] = useState("");
  const [emriPrindit, setEmriPrindit] = useState("");
  const [mbimeri, setMbiemri] = useState("");
  const [nrPersonal, setNrPersonal] = useState("");
  const [emailPersonal, setEmailPersonal] = useState("");
  const [kodiFinanciar, setKodiFinanciar] = useState("");
  const [nrKontaktit, setNrKontaktit] = useState("");
  const [gjinia, setGjinia] = useState("M");
  const [dataLindjes, setDataLindejs] = useState(null);
  const [qyteti, setQyteti] = useState("");
  const [adresa, setAdresa] = useState("");
  const [shteti, setShteti] = useState("Kosovë");
  const [llojiKontrates, setLlojiKontrates] = useState(1);
  const [llojiZbritjes, setLlojiZbritjes] = useState("");
  const [zipKodi, setZipKodi] = useState("");
  const [departamentiID, setDepartamentiID] = useState("");
  const [niveliStudimitID, setNiveliStudimitID] = useState("");
  const [LlojiRegjistrimit, setLlojiRegjistrimit] = useState("I Rregullt");
  const [vitiAkademikRegjistrim, setVitiAkademikRegjistrim] = useState("");

  const [departamentet, setDepartamentet] = useState([]);
  const [niveletEStudimit, setNiveletEStudimit] = useState([]);
  const [zbritjet, setZbritjet] = useState([]);

  const [roletSelektim, setRoletSelektim] = useState([]);

  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");

  const DitaSotme = new Date();
  const DatelindjaMaxLejuar = new Date(
    DitaSotme.getFullYear() - 17,
    DitaSotme.getMonth(),
    DitaSotme.getDate()
  );

  const getToken = localStorage.getItem("token");
  const getID = localStorage.getItem("id");
  const navigate = useNavigate();

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const vendosDepartamentet = async () => {
      try {
        const rolet = await axios.get(
          `https://localhost:7251/api/Perdoruesi/shfaqSipasID?idUserAspNet=${getID}`,
          authentikimi
        );
        if (!rolet.data.rolet.includes("Administrat")) {
          navigate("/NukKeniAkses");
        }

        const departamentet = await axios.get(
          `https://localhost:7251/api/Administrata/shfaqDepartamentet`,
          authentikimi
        );
        const vitiAkademikRegjistrim = await axios.get(
          `https://localhost:7251/api/Administrata/GjeneroVitinAkademik`,
          authentikimi
        );
        setDepartamentet(departamentet.data);
        setVitiAkademikRegjistrim(vitiAkademikRegjistrim.data);
      } catch (err) {
        console.log(err);
      }
    };

    const vendosZbritjet = async () => {
      try {
        const zbritjet = await axios.get(
          `https://localhost:7251/api/Administrata/ShfaqZbritjet`,
          authentikimi
        );

        setZbritjet(zbritjet.data);
      } catch (err) {
        console.log(err);
      }
    };

    vendosDepartamentet();
    vendosZbritjet();
  }, []);

  useEffect(() => {
    const vendosNiveletEStudimit = async () => {
      try {
        const niveletEStudimit = await axios.get(
          `https://localhost:7251/api/Administrata/shfaqNiveletStudimitGjatAplikimit?departamentiID=${departamentiID}`,
          authentikimi
        );
        console.log(niveletEStudimit.data);
        setNiveletEStudimit(niveletEStudimit.data);
      } catch (err) {
        console.log(err);
      }
    };

    vendosNiveletEStudimit();
  }, [departamentiID]);

  const handleChange = (setState) => (event) => {
    setState(event.target.value);
  };

  const handleShtetiChange = (event) => {
    setShteti(event.target.value);
  };

  const handleLlojiKontratesChange = (event) => {
    setLlojiKontrates(event.target.value);
  };

  const handleLlojiZbritjesChange = (event) => {
    setLlojiZbritjes(event.target.value);
  };

  const handleDepartamentiChange = (event) => {
    setDepartamentiID(event.target.value);
  };

  const handleNiveliStudimiChange = (event) => {
    setNiveliStudimitID(event.target.value);
  };

  const handleLlojiRegjistrimitChange = (event) => {
    setLlojiRegjistrimit(event.target.value);
  };

  const handleGjiniaChange = (event) => {
    setGjinia(event.target.value);
  };

  function isNullOrEmpty(value) {
    return value === null || value === "" || value === undefined;
  }

  async function CreateAcc(e) {
    e.preventDefault();

    if (
      isNullOrEmpty(emri) ||
      isNullOrEmpty(mbimeri) ||
      isNullOrEmpty(emriPrindit) ||
      isNullOrEmpty(nrPersonal) ||
      isNullOrEmpty(emailPersonal) ||
      isNullOrEmpty(nrKontaktit) ||
      isNullOrEmpty(dataLindjes) ||
      isNullOrEmpty(adresa) ||
      isNullOrEmpty(qyteti) ||
      departamentiID === 0 ||
      niveliStudimitID === 0
    ) {
      setPershkrimiMesazhit(
        "<strong>Ju lutemi plotesoni te gjitha fushat me *</strong>"
      );
      setTipiMesazhit("danger");
      setShfaqMesazhin(true);
    } else {
      const NrPeronsalRKSREGEX = /^\d{10}$/;
      const NrPeronsalALBREGEX = /^[A-Za-z]\d{8}[A-Za-z]$/;
      const NrPeronsalNMKREGEX = /^\d{13}$/;
      const telefoniREGEX = /^(?:\+\d{11}|\d{9})$/;
      const emailREGEX = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
      const VetemShkronjaREGEX = /^[a-zA-ZçëÇË -]+$/;

      var kodiFinanciar = await axios.get(
        `https://localhost:7251/api/Administrata/gjeneroKodinFinanciar?departamentiID=${departamentiID}&niveliStudimitID=${niveliStudimitID}`, authentikimi
      );

      if (shteti == "Kosovë" && !NrPeronsalRKSREGEX.test(nrPersonal)) {
        setPershkrimiMesazhit(
          "<strong>Numri Personal duhet te jete ne formatin NNNNNNNNNN! N - Numer, 10 Karaktere</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (shteti == "Shqipëri" && !NrPeronsalALBREGEX.test(nrPersonal)) {
        setPershkrimiMesazhit(
          "<strong>Numri Personal duhet te jete ne formatin LNNNNNNNNL! N - Numer & L - Shkronje, 10 Karaktere</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (
        shteti == "Maqedoni e Veriut" &&
        !NrPeronsalNMKREGEX.test(nrPersonal)
      ) {
        setPershkrimiMesazhit(
          "<strong>Numri Personal duhet te jete ne formatin NNNNNNNNNNNNN! N - Numer, 13 Karaktere</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (!emailREGEX.test(emailPersonal)) {
        setPershkrimiMesazhit("<strong>Ky email nuk eshte valid!</strong>");
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (!telefoniREGEX.test(nrKontaktit)) {
        setPershkrimiMesazhit(
          "Numri telefonit duhet te jete ne formatin: <strong>045123123 ose +38343123132</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (!VetemShkronjaREGEX.test(emri)) {
        setPershkrimiMesazhit(
          "<strong>Emri mund te permbaje vetem shkronja, hapesira dhe -!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (!VetemShkronjaREGEX.test(emriPrindit)) {
        setPershkrimiMesazhit(
          "<strong>Emri Prindit mund te permbaje vetem shkronja, hapesira dhe -!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (!VetemShkronjaREGEX.test(mbimeri)) {
        setPershkrimiMesazhit(
          "<strong>Mbiemri mund te permbaje vetem shkronja, hapesira dhe -!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (!VetemShkronjaREGEX.test(qyteti)) {
        setPershkrimiMesazhit(
          "<strong>Qyteti mund te permbaje vetem shkronja, hapesira dhe -!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (
        (shteti == "Maqedoni e Veriut" || shteti == "Shqipëri") &&
        (zipKodi < 1000 || zipKodi > 9999)
      ) {
        setPershkrimiMesazhit(
          "<strong>Kodi Postar duhet te permbaj vetem 4 numra!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (shteti == "Kosovë" && (zipKodi < 10000 || zipKodi > 99999)) {
        setPershkrimiMesazhit(
          "<strong>Kodi Postar duhet te permbaj vetem 5 numra!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else {
        axios
          .post(
            "https://localhost:7251/api/Administrata/KrijoniAplikimERi",
            {
              aplikimetEReja: {
                Emri: emri,
                Mbiemri: mbimeri,
                NrPersonal: nrPersonal.toString(),
                EmriPrindit: emriPrindit,
                EmailPersonal: emailPersonal,
                NrKontaktit: nrKontaktit,
                Qyteti: qyteti,
                ZipKodi: zipKodi,
                Adresa: adresa,
                Shteti: shteti,
                Gjinia: gjinia,
                DataLindjes: dataLindjes,
                KodiFinanciar: kodiFinanciar.data.toString(),
                DepartamentiID: departamentiID,
                NiveliStudimitID: niveliStudimitID,
                VitiAkademikRegjistrim: vitiAkademikRegjistrim,
                LlojiRegjistrimit: LlojiRegjistrimit,
                llojiKontrates: llojiKontrates,
                zbritjaID: llojiZbritjes,
              },
            },
            authentikimi
          )
          .then(() => {
            setPershkrimiMesazhit(
              "<strong>Aplikimi u pranua! Ju lutem keshilloni studentit te beje pagesen permes kodit financiar.</strong>"
            );
            setTipiMesazhit("success");
            setShfaqMesazhin(true);

            setEmri("");
            setEmriPrindit("");
            setMbiemri("");
            setNrPersonal("");
            setEmailPersonal("");
            setKodiFinanciar("");
            setNrKontaktit("");
            setGjinia("M");
            setDataLindejs(null);
            setQyteti("");
            setAdresa("");
            setShteti("Kosovë");
            setZipKodi("");
            setDepartamentiID("");
            setNiveliStudimitID("");
            setLlojiRegjistrimit("I Rregullt");
            setVitiAkademikRegjistrim("");
          })
          .catch((error) => {
            console.error(error);
            setPershkrimiMesazhit(
              "<strong>Ju lutemi kontaktoni me stafin pasi ndodhi nje gabim ne server!</strong>"
            );
            setTipiMesazhit("danger");
            setShfaqMesazhin(true);
          });
      }
    }
  }

  return (
    <>
      <Helmet>
        <title>Krijoni Aplikim e Ri | UBT SMIS</title>
      </Helmet>

      <div className="sign-up mb-4">
        {shfaqMesazhin && (
          <Mesazhi
            setShfaqMesazhin={setShfaqMesazhin}
            pershkrimi={pershkrimiMesazhit}
            tipi={tipiMesazhit}
          />
        )}
        <Form className="mx-3">
          <h3>Informatat Personale</h3>
          <Row className="mb-3 gap-1">
            <Form.Group as={Col} className="p-0" controlId="formGridName">
              <Form.Label>
                Emri<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                type="name"
                placeholder="Emri Studentit"
                value={emri}
                onChange={handleChange(setEmri)}
                required
                autoFocus
                disabled={
                  vitiAkademikRegjistrim == "Nuk ka afat te hapur"
                    ? true
                    : false
                }
              />
            </Form.Group>

            <Form.Group as={Col} className="p-0" controlId="formGridLastName">
              <Form.Label>
                Emri Prindit<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                type="text"
                placeholder="Emri Prindit te Studentit"
                value={emriPrindit}
                onChange={handleChange(setEmriPrindit)}
                required
              />
            </Form.Group>
            <Form.Group as={Col} className="p-0" controlId="formGridLastName">
              <Form.Label>
                Mbiemri<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                type="text"
                placeholder="Mbiemri Studentit"
                value={mbimeri}
                onChange={handleChange(setMbiemri)}
                required
              />
            </Form.Group>
          </Row>
          <Row className="mb-3 gap-1">
            <Form.Group as={Col} className="p-0" controlId="formGridName">
              <Form.Label>
                Numri Personal <span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                type={shteti == "Shqipëri" ? "text" : "number"}
                placeholder="1100110011"
                value={nrPersonal}
                onChange={handleChange(setNrPersonal)}
                required
                autoFocus
              />
            </Form.Group>
            <Form.Group as={Col} className="p-0" controlId="formGridLastName">
              <Form.Label>Gjinia</Form.Label>
              <Form.Select value={gjinia} onChange={handleGjiniaChange}>
                <option selected value="M">
                  Mashkull
                </option>
                <option value="F">Femer</option>
              </Form.Select>
            </Form.Group>
            <Form.Group as={Col} className="p-0" controlId="formGridLastName">
              <Form.Label>
                Data Lindjes<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Col>
                <DatePicker
                  selected={dataLindjes}
                  onChange={(date) => setDataLindejs(date)}
                  className="form-control"
                  dateFormat="dd/MM/yyyy"
                  maxDate={DatelindjaMaxLejuar}
                />
              </Col>
            </Form.Group>
          </Row>
          <hr />
          <h3>Te Dhenat Ndihmese</h3>
          <Row className="mb-3 gap-1">
            <Form.Group as={Col} className="p-0" controlId="formGridLastName">
              <Form.Label>
                Email Personal<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                type="text"
                placeholder="email@ubt-uni.net"
                value={emailPersonal}
                onChange={handleChange(setEmailPersonal)}
                required
              />
            </Form.Group>
            <Form.Group as={Col} className="p-0" controlId="formGridName">
              <Form.Label>
                Nr Kontaktit<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                placeholder="045123123 ose +38343123132"
                value={nrKontaktit}
                onChange={handleChange(setNrKontaktit)}
              />
            </Form.Group>
          </Row>
          <Row className="mb-3 gap-1">
            <Form.Group as={Col} className="p-0" controlId="formGridAdresa">
              <Form.Label>
                Adresa<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                placeholder="Agim Bajrami 60"
                value={adresa}
                onChange={handleChange(setAdresa)}
              />
            </Form.Group>
            <Form.Group as={Col} className="p-0" controlId="formGridQyteti">
              <Form.Label>
                Qyteti<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                placeholder="Kaçanik"
                value={qyteti}
                onChange={handleChange(setQyteti)}
              />
            </Form.Group>
            <Form.Group as={Col} className="p-0" controlId="formGridState">
              <Form.Label>
                Shteti<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Select value={shteti} onChange={handleShtetiChange}>
                <option selected>Kosovë</option>
                <option>Shqipëri</option>
                <option>Maqedoni e Veriut</option>
              </Form.Select>
            </Form.Group>

            <Form.Group as={Col} className="p-0" controlId="formGridZip">
              <Form.Label>Kodi Postar</Form.Label>
              <Form.Control
                type="number"
                placeholder="71000"
                value={zipKodi}
                onChange={handleChange(setZipKodi)}
              />
            </Form.Group>
          </Row>
          <hr />
          <h3>Te Dhenat Studimit</h3>

          <Row className="mb-3 gap-1">
            <Form.Group as={Col} className="p-0" controlId="formGridAdresa">
              <Form.Label>
                Departamenti<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Select
                value={departamentiID}
                onChange={handleDepartamentiChange}>
                <option value={0} hidden selected>
                  Zgjedhni Departamentin
                </option>
                {departamentet &&
                  departamentet.map((departamenti) => (
                    <option value={departamenti.departamentiID}>
                      {departamenti.emriDepartamentit}
                    </option>
                  ))}
              </Form.Select>
            </Form.Group>
            <Form.Group as={Col} className="p-0" controlId="formGridQyteti">
              <Form.Label>
                Niveli Studimit<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Select
                value={niveliStudimitID}
                onChange={handleNiveliStudimiChange}
                disabled={niveletEStudimit.length > 0 ? false : true}>
                <option value={0} hidden selected>
                  Zgjedhni Nivelin e Studimit
                </option>
                {niveletEStudimit &&
                  niveletEStudimit.map((niveliIStudimit) => (
                    <option
                      value={niveliIStudimit.niveliStudimeve.niveliStudimeveID}>
                      {niveliIStudimit.niveliStudimeve.emriNivelitStudimeve} -{" "}
                      {
                        niveliIStudimit.niveliStudimeve
                          .shkurtesaEmritNivelitStudimeve
                      }
                    </option>
                  ))}
              </Form.Select>
            </Form.Group>
            <Form.Group as={Col} className="p-0" controlId="formGridState">
              <Form.Label>Lloji Regjistrimit</Form.Label>
              <Form.Select
                value={LlojiRegjistrimit}
                onChange={handleLlojiRegjistrimitChange}>
                <option selected>I Rregullt</option>
                <option>Me Korrespodencë</option>
              </Form.Select>
            </Form.Group>
          </Row>
          <Row className="mb-3 gap-1">
            <Form.Group as={Col} className="p-0" controlId="formGridState">
              <Form.Label>Lloji Kontrates</Form.Label>
              <Form.Select
                value={llojiKontrates}
                onChange={handleLlojiKontratesChange}>
                <option selected value={1}>
                  1 Vjeqare
                </option>
                <option value={3}>3 Vjeqare</option>
              </Form.Select>
            </Form.Group>
            <Form.Group as={Col} className="p-0" controlId="formGridAdresa">
              <Form.Label>Zbritja</Form.Label>
              <Form.Select
                value={llojiZbritjes}
                onChange={handleLlojiZbritjesChange}>
                <option value={null} hidden selected>
                  Zgjedhni Zbritjen
                </option>
                {zbritjet &&
                  zbritjet.map((zbritja) => (
                    <option value={zbritja.zbritjaID}>
                      {zbritja.emriZbritjes} - Zbritja:{" "}
                      {parseFloat(zbritja.zbritja).toFixed(2)} % - Kohezgjatja:{" "}
                      {zbritja.llojiZbritjes}
                    </option>
                  ))}
              </Form.Select>
            </Form.Group>
          </Row>

          <hr />

          <div
            style={{ display: "flex", flexDirection: "column", width: "30%" }}>
            <Button variant="primary" type="submit" onClick={CreateAcc}>
              Përfundo Aplikimin
            </Button>
          </div>
        </Form>
      </div>
    </>
  );
};

export default KrijoAplikiminERi;
