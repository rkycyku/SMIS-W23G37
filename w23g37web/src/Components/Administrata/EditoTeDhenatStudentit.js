import { useState, useRef, useEffect } from "react";
import axios from "axios";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import Modal from "react-bootstrap/Modal";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faXmark } from "@fortawesome/free-solid-svg-icons";
import { faPenToSquare } from "@fortawesome/free-solid-svg-icons";
import { Row, Col } from "react-bootstrap";
import Mesazhi from "../../Components/TeTjera/layout/Mesazhi";
import DatePicker from "react-datepicker";

function EditoTeDhenatStudentit(props) {
  const [teDhenat, setTeDhenat] = useState([]);
  const [dateLindja, setDateLindja] = useState(new Date(Date.now()));

  const [fushatEZbrazura, setFushatEZbrazura] = useState(false);

  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const shfaqBanken = async () => {
      try {
        const teDhenat = await axios.get(
          `https://localhost:7251/api/Perdoruesi/ShfaqTeDhenatPerEditim?id=` +
            props.id,
          authentikimi
        );
        setTeDhenat(teDhenat.data);
        setDateLindja(new Date(teDhenat.data.dataLindjes));
        console.log(teDhenat.data);
      } catch (err) {
        console.log(err);
      }
    };

    shfaqBanken();
  }, []);

  const handleChange = (propertyName) => (event) => {
    setTeDhenat((prev) => ({
      ...prev,
      [propertyName]: event.target.value,
    }));
  };

  const handleShtetiChange = (event) => {
    setTeDhenat((prev) => ({ ...prev, shteti: event }));
  };

  const handleDateChange = (date) => {
    setDateLindja(date);
    // Your additional logic on date change, if needed
  };

  const DitaSotme = new Date();
  const DatelindjaMaxLejuar = new Date(
    DitaSotme.getFullYear() - 17,
    DitaSotme.getMonth(),
    DitaSotme.getDate()
  );

  function isNullOrEmpty(value) {
    return value === null || value === "" || value === undefined;
  }

  async function handleSubmit() {
    try {
      const telefoniREGEX = /^(?:\+\d{11}|\d{9})$/;
      const emailREGEX = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
      const VetemShkronjaREGEX = /^[a-zA-ZçëÇË -]+$/;

      if (!emailREGEX.test(teDhenat.email)) {
        setPershkrimiMesazhit("<strong>Ky email nuk eshte valid!</strong>");
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (!telefoniREGEX.test(teDhenat.nrKontaktit)) {
        setPershkrimiMesazhit(
          "Numri telefonit duhet te jete ne formatin: <strong>045123123 ose +38343123132</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (!VetemShkronjaREGEX.test(teDhenat.emri)) {
        setPershkrimiMesazhit(
          "<strong>Emri mund te permbaje vetem shkronja, hapesira dhe -!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (!VetemShkronjaREGEX.test(teDhenat.emriPrindit)) {
        setPershkrimiMesazhit(
          "<strong>Emri Prindit mund te permbaje vetem shkronja, hapesira dhe -!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (!VetemShkronjaREGEX.test(teDhenat.mbimeri)) {
        setPershkrimiMesazhit(
          "<strong>Mbiemri mund te permbaje vetem shkronja, hapesira dhe -!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (!VetemShkronjaREGEX.test(teDhenat.qyteti)) {
        setPershkrimiMesazhit(
          "<strong>Qyteti mund te permbaje vetem shkronja, hapesira dhe -!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (
        (teDhenat.shteti == "Maqedoni e Veriut" ||
          teDhenat.shteti == "Shqipëri") &&
        (teDhenat.zipKodi < 1000 || teDhenat.zipKodi > 9999)
      ) {
        setPershkrimiMesazhit(
          "<strong>Kodi Postar duhet te permbaj vetem 4 numra!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else if (
        teDhenat.shteti == "Kosovë" &&
        (teDhenat.zipKodi < 10000 || teDhenat.zipKodi > 99999)
      ) {
        setPershkrimiMesazhit(
          "<strong>Kodi Postar duhet te permbaj vetem 5 numra!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      } else {
        await axios
          .put(
            `https://localhost:7251/api/Administrata/PerditesoniTeDhenatStudentit?studentiID=` +
              props.id,
            {
              teDhenatPerdoruesit: {
                emriPrindit: teDhenat.emriPrindit,
                emailPersonal: teDhenat.email,
                nrKontaktit: teDhenat.nrKontaktit,
                qyteti: teDhenat.qyteti,
                zipKodi: teDhenat.zipKodi,
                adresa: teDhenat.adresa,
                shteti: teDhenat.shteti,
                dataLindjes: dateLindja,
              },
              perdoruesi: {
                emri: teDhenat.emri,
                mbiemri: teDhenat.mbiemri,
                aspNetUserId: props.id,
              },
            },
            authentikimi
          )
          .then((x) => {
            props.setTipiMesazhit("success");
            props.setPershkrimiMesazhit("Te Dhenat u Perditesua me sukses!");
            props.perditesoTeDhenat();
            props.hide();
            props.shfaqmesazhin();
          })
          .catch((error) => {
            console.error("Gabime gjate perditesimit te te dhenave:", error);
            setTipiMesazhit("danger");
            setPershkrimiMesazhit(
              "Ndodhi nje gabim gjate perditesimit te te Dhenave!"
            );
            setShfaqMesazhin(true);
          });
      }
    } catch (error) {
      console.error(error);
    }
  }

  const handleKontrolli = () => {
    if (
      isNullOrEmpty(teDhenat.emri) ||
      isNullOrEmpty(teDhenat.emriPrindit) ||
      isNullOrEmpty(teDhenat.mbiemri) ||
      isNullOrEmpty(teDhenat.nrKontaktit) ||
      isNullOrEmpty(teDhenat.qyteti) ||
      isNullOrEmpty(teDhenat.shteti) ||
      isNullOrEmpty(teDhenat.zipKodi) ||
      isNullOrEmpty(teDhenat.email)
    ) {
      setFushatEZbrazura(true);
    } else {
      handleSubmit();
    }
  };

  const dataL = new Date(teDhenat.dataLindjes);

  return (
    <>
      {fushatEZbrazura && (
        <Modal
          size="sm"
          className="mt-3"
          show={fushatEZbrazura}
          onHide={() => setFushatEZbrazura(false)}>
          <Modal.Header closeButton>
            <Modal.Title style={{ color: "red" }} as="h6">
              Ndodhi nje gabim
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <strong style={{ fontSize: "10pt" }}>
              Ju lutemi plotesoni te gjitha fushat me{" "}
              <span style={{ color: "red" }}>*</span>
            </strong>
          </Modal.Body>
          <Modal.Footer>
            <Button
              size="sm"
              onClick={() => setFushatEZbrazura(false)}
              variant="secondary">
              Mbylle <FontAwesomeIcon icon={faXmark} />
            </Button>
          </Modal.Footer>
        </Modal>
      )}
      {shfaqMesazhin && (
        <Mesazhi
          setShfaqMesazhin={setShfaqMesazhin}
          pershkrimi={pershkrimiMesazhit}
          tipi={tipiMesazhit}
        />
      )}
      <Modal
        className="modalEditShto"
        show={props.show}
        onHide={props.hide}
        size="lg">
        <Modal.Header closeButton>
          <Modal.Title>Perditeso Te Dhenat e Studentit</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Row className="mb-3">
              <Form.Group as={Col} controlId="exampleForm.ControlInput1">
                <Form.Label>
                  Emri<span style={{ color: "red" }}>*</span>
                </Form.Label>
                <Form.Control
                  onChange={handleChange("emri")}
                  value={teDhenat.emri}
                  type="text"
                  placeholder="Emri"
                  autoFocus
                />
              </Form.Group>
              <Form.Group as={Col} controlId="exampleForm.ControlInput1">
                <Form.Label>
                  Emri Prindit<span style={{ color: "red" }}>*</span>
                </Form.Label>
                <Form.Control
                  onChange={handleChange("emriPrindit")}
                  value={teDhenat.emriPrindit}
                  type="text"
                  placeholder="Emri Prindit"
                />
              </Form.Group>
              <Form.Group as={Col} controlId="exampleForm.ControlInput1">
                <Form.Label>
                  Mbiemri<span style={{ color: "red" }}>*</span>
                </Form.Label>
                <Form.Control
                  onChange={handleChange("mbiemri")}
                  value={teDhenat.mbiemri}
                  type="text"
                  placeholder="Mbiemri"
                />
              </Form.Group>
            </Row>
            <Row className="mb-3">
              <Form.Group as={Col} controlId="exampleForm.ControlInput1">
                <Form.Label>Adresa</Form.Label>
                <Form.Control
                  onChange={handleChange("adresa")}
                  value={teDhenat.adresa}
                  type="text"
                  placeholder="Adresa"
                />
              </Form.Group>
              <Form.Group as={Col} controlId="exampleForm.ControlInput1">
                <Form.Label>
                  Vendbanimi<span style={{ color: "red" }}>*</span>
                </Form.Label>
                <Form.Control
                  onChange={handleChange("qyteti")}
                  value={teDhenat.qyteti}
                  type="text"
                  placeholder="Vendbanimi"
                />
              </Form.Group>
              <Form.Group as={Col} controlId="exampleForm.ControlInput1">
                <Form.Label>
                  Shteti<span style={{ color: "red" }}>*</span>
                </Form.Label>
                <select
                  placeholder="Shteti"
                  className="form-select"
                  value={teDhenat.shteti}
                  onChange={(e) => handleShtetiChange(e.target.value)}>
                  <option selected>Kosovë</option>
                  <option>Shqipëri</option>
                  <option>Maqedoni e Veriut</option>
                </select>
              </Form.Group>
              <Form.Group as={Col} controlId="exampleForm.ControlTextarea1">
                <Form.Label>
                  Zip Kodi<span style={{ color: "red" }}>*</span>
                </Form.Label>
                <Form.Control
                  onChange={handleChange("zipKodi")}
                  value={teDhenat.zipKodi}
                  type="number"
                  placeholder="Zip Kodi"
                />
              </Form.Group>
            </Row>
            <Row className="mb-3">
              <Form.Group as={Col} controlId="exampleForm.ControlInput1">
                <Form.Label>
                  Telefoni<span style={{ color: "red" }}>*</span>
                </Form.Label>
                <Form.Control
                  onChange={handleChange("nrKontaktit")}
                  value={teDhenat.nrKontaktit}
                  type="text"
                  placeholder="Telefoni"
                />
              </Form.Group>
              <Form.Group as={Col} controlId="exampleForm.ControlTextarea1">
                <Form.Label>
                  Email Privat<span style={{ color: "red" }}>*</span>
                </Form.Label>
                <Form.Control
                  onChange={handleChange("email")}
                  value={teDhenat.email}
                  type="text"
                  placeholder="Email Privat"
                />
              </Form.Group>
              <Form.Group as={Col} className="p-0" controlId="formGridLastName">
                <Form.Label>
                  Data Lindjes<span style={{ color: "red" }}>*</span>
                </Form.Label>
                <Col>
                  <DatePicker
                    selected={dateLindja}
                    onChange={handleDateChange}
                    className="form-control"
                    dateFormat="dd/MM/yyyy"
                  />
                </Col>
              </Form.Group>
            </Row>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={props.hide}>
            Anulo <FontAwesomeIcon icon={faXmark} />
          </Button>
          <Button
            style={{ backgroundColor: "#009879", border: "none" }}
            onClick={handleKontrolli}>
            Perditeso <FontAwesomeIcon icon={faPenToSquare} />
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default EditoTeDhenatStudentit;
