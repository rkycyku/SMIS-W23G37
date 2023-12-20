import { React, useState, useRef } from "react";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import Modal from "react-bootstrap/Modal";
import axios from "axios";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faXmark, faBan, faL, faAdd } from "@fortawesome/free-solid-svg-icons";
import { useEffect } from "react";

const ShtoBanken = (props) => {
  const [llojiBankes, setLlojiBankes] = useState("Hyrese/Dalese");
  const [ibanFillimi, setIbanFillimi] = useState("");
  const [valuta, setValuta] = useState("Euro");
  const [swiftKodi, setSwiftKodi] = useState("");
  const [bicKodi, setBicKodi] = useState([]);
  const [adresaBankes, setAdresaBankes] = useState([]);
  const [numriLlogaris, setNumriLlogaris] = useState(null);
  const [kodiBankes, setKodiBankes] = useState("");
  const [emriBankes, setEmriBankes] = useState("");

  const [perditeso, setPerditeso] = useState("");

  const [produktet, setProduktet] = useState([]);
  const [kontrolloProduktin, setKontrolloProduktin] = useState(false);
  const [fushatEZbrazura, setFushatEZbrazura] = useState(false);

  useEffect(() => {
    const vendosProduktet = async () => {
      try {
        const produktet = await axios.get(
          `https://localhost:7285/api/Produkti/Products`,
          authentikimi
        );
        setProduktet(produktet.data);
      } catch (err) {
        console.log(err);
      }
    };

    vendosProduktet();
  }, [perditeso]);

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  const handleChange = (setState) => (event) => {
    setState(event.target.value);
  };

  const handleLlojiBankesChange = (event) => {
    setLlojiBankes(event);
  };

  const handleValutaChange = (event) => {
    setValuta(event);
  };

  function isNullOrEmpty(value) {
    return value === null || value === "" || value === undefined;
  }

  async function handleSubmit() {
    try {
      await axios
        .post(
          "https://localhost:7251/api/Financat/ShtoniBanken",
          {
            banka: {
              emriBankes: emriBankes,
              kodiBankes: kodiBankes,
              numriLlogaris: numriLlogaris.toString(),
              adresaBankes: adresaBankes,
              bicKodi: bicKodi.toString(),
              swiftKodi: swiftKodi,
              valuta: valuta,
              ibanFillimi: ibanFillimi,
              llojiBankes: llojiBankes,
            },
          },
          authentikimi
        )
        .then(async (response) => {
          props.setTipiMesazhit("success");
          props.setPershkrimiMesazhit("Banka u insertua me sukses!");
          props.perditesoTeDhenat();
          props.hide();
          props.shfaqmesazhin();
        })
        .catch((error) => {
          console.log(error);
        });
    } catch (error) {
      console.error(error);
    }
  }

  const handleKontrolli = () => {
    if (isNullOrEmpty(emriBankes) || isNullOrEmpty(kodiBankes)) {
      setFushatEZbrazura(true);
    } else {
      handleSubmit();
    }
  };

  return (
    <>
      {fushatEZbrazura && (
        <Modal
          size="sm"
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
      <Modal className="modalEditShto" show={props.show} onHide={props.hide}>
        <Modal.Header closeButton>
          <Modal.Title>Shtoni Banken</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>
                Emri Bankes<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                onChange={handleChange(setEmriBankes)}
                value={emriBankes}
                type="text"
                placeholder="Emri Bankes"
                autoFocus
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>
                Kodi Bankes<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                onChange={handleChange(setKodiBankes)}
                value={kodiBankes}
                type="text"
                placeholder="Kodi Bankes"
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>
                Numri Llogaris<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                onChange={handleChange(setNumriLlogaris)}
                value={numriLlogaris}
                type="text"
                placeholder="Numri Llogaris"
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>Adresa Bankes</Form.Label>
              <Form.Control
                onChange={handleChange(setAdresaBankes)}
                value={adresaBankes}
                as="textarea"
                placeholder="Adresa Bankes"
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>
                BIC Kodi<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                onChange={handleChange(setBicKodi)}
                value={bicKodi}
                type="text"
                placeholder="BIC Kodi"
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>
                SWIFT Kodi<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                onChange={handleChange(setSwiftKodi)}
                value={swiftKodi}
                type="text"
                placeholder="SWIFT Kodi"
              />
            </Form.Group>
            <Form.Group
              className="mb-3"
              controlId="exampleForm.ControlTextarea1">
              <Form.Label>
                Valuta<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <select
                placeholder="Valuta"
                className="form-select"
                value={valuta}
                onChange={(e) => handleValutaChange(e.target.value)}>
                <option defaultValue selected value="Euro">
                  Euro - €
                </option>
                <option value="Dollar">Dollar - $</option>
                <option value="Franga Zvicerane">Franga Zvicerane - CHF</option>
              </select>
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>
                Kodi IBAN<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                onChange={handleChange(setIbanFillimi)}
                value={ibanFillimi}
                type="text"
                placeholder="XK05"
              />
            </Form.Group>
            <Form.Group
              className="mb-3"
              controlId="exampleForm.ControlTextarea1">
              <Form.Label>
                Lloji Bankes<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <select
                placeholder="Lloji Bankes"
                className="form-select"
                value={llojiBankes}
                onChange={(e) => handleLlojiBankesChange(e.target.value)}>
                <option defaultValue selected value="Hyrese/Dalese">
                  Hyrese/Dalese
                </option>
                <option value="Hyrese">Hyrese</option>
                <option value="Dalese">Dalese</option>
              </select>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={props.hide}>
            Mbylle <FontAwesomeIcon icon={faXmark} />
          </Button>
          <Button
            style={{ backgroundColor: "#009879", border: "none" }}
            onClick={handleKontrolli}>
            Ruaj Banken <FontAwesomeIcon icon={faAdd} />
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

export default ShtoBanken;
