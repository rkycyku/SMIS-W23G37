import { useState, useRef, useEffect } from "react";
import axios from "axios";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import Modal from "react-bootstrap/Modal";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faXmark } from "@fortawesome/free-solid-svg-icons";
import { faPenToSquare } from '@fortawesome/free-solid-svg-icons';


function EditoBanken(props) {
  const [banka, setBanka] = useState([]);

  const [perditeso, setPerditeso] = useState("");
  const [produktet, setProduktet] = useState([]);
  const [kontrolloProduktin, setKontrolloProduktin] = useState(false);
  const [konfirmoProduktin, setKonfirmoProduktin] = useState(false);
  const [fushatEZbrazura, setFushatEZbrazura] = useState(false);

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const shfaqBanken = async () => {
      try {
        const banka = await axios.get(`https://localhost:7251/api/Financat/ShfaqBankenNgaID?bankaID=` + props.id, authentikimi);
        setBanka(banka.data);

      } catch (err) {
        console.log(err);
      }
    };
    
    shfaqBanken();
  }, []);


  const handleChange = (propertyName) => (event) => {
    setBanka((prev) => ({
      ...prev,
      [propertyName]: event.target.value,
    }));
  };

  const handleLlojiBankesChange = (event) => {
    setBanka((prev) => ({ ...prev, llojiBankes: event }));

  };

  const handleValutaChange = (event) => {
    setBanka((prev) => ({ ...prev, valuta: event }));
  };


  function isNullOrEmpty(value) {
    return value === null || value === "" || value === undefined;
  }

  async function handleSubmit() {
    
    try {
      await axios.put(`https://localhost:7251/api/Financat/PerditesoniBanken?bankaID=` + props.id, {
            banka: {
              emriBankes: banka.emriBankes,
              kodiBankes: banka.kodiBankes,
              numriLlogaris: banka.numriLlogaris.toString(),
              adresaBankes: banka.adresaBankes,
              bicKodi: banka.bicKodi.toString(),
              swiftKodi: banka.swiftKodi,
              valuta: banka.valuta,
              ibanFillimi: banka.ibanFillimi,
              llojiBankes: banka.llojiBankes,
            },
          },
          authentikimi
        )
        .then(x => {

          props.setTipiMesazhit("success");
          props.setPershkrimiMesazhit("Banka u Perditesua me sukses!")
          props.perditesoTeDhenat();
          props.hide();
          props.shfaqmesazhin();
        })
        .catch(error => {
          console.error('Error saving the bank:', error);
          props.setTipiMesazhit("danger");
          props.setPershkrimiMesazhit("Ndodhi nje gabim gjate perditesimit te bankes!")
          props.perditesoTeDhenat();
          props.shfaqmesazhin();
        });
    } catch (error) {
      console.error(error);
    }
  }

  const handleKontrolli = () => {
    if (isNullOrEmpty(banka.emriBankes) || isNullOrEmpty(banka.kodiBankes)) {
      setFushatEZbrazura(true);
    } else {
      handleSubmit();
    }
  };

  return (
    <>
      {fushatEZbrazura &&
        <Modal size="sm" show={fushatEZbrazura} onHide={() => setFushatEZbrazura(false)}>
          <Modal.Header closeButton>
            <Modal.Title style={{ color: "red" }} as="h6">Ndodhi nje gabim</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <strong style={{ fontSize: "10pt" }}>Ju lutemi plotesoni te gjitha fushat me <span style={{ color: "red" }}>*</span></strong>
          </Modal.Body>
          <Modal.Footer>
            <Button size="sm" onClick={() => setFushatEZbrazura(false)} variant="secondary">
              Mbylle <FontAwesomeIcon icon={faXmark} />
            </Button >
          </Modal.Footer>

        </Modal>
      }
      <Modal className="modalEditShto" show={props.show} onHide={props.hide}>
        <Modal.Header closeButton>
          <Modal.Title>Edito Banken</Modal.Title>
        </Modal.Header>
        <Modal.Body>
        <Form>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>Emri Bankes<span style={{ color: "red" }}>*</span></Form.Label>
              <Form.Control
                onChange={handleChange('emriBankes')}
                value={banka.emriBankes}
                type="text"
                placeholder="Emri Bankes"
                autoFocus
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>Kodi Bankes<span style={{ color: "red" }}>*</span></Form.Label>
              <Form.Control
                onChange={handleChange('kodiBankes')}
                value={banka.kodiBankes}
                type="text"
                placeholder="Kodi Bankes"
                autoFocus
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>Numri Llogaris<span style={{ color: "red" }}>*</span></Form.Label>
              <Form.Control
                onChange={handleChange('numriLlogaris')}
                value={banka.numriLlogaris}
                type="text"
                placeholder="Numri Llogaris"
                autoFocus
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>Adresa Bankes</Form.Label>
              <Form.Control
            onChange={handleChange('adresaBankes')}
                value={banka.adresaBankes}
                as="textarea"
                placeholder="Adresa Bankes"
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>BIC Kodi<span style={{ color: "red" }}>*</span></Form.Label>
              <Form.Control
                onChange={handleChange('bicKodi')}
                value={banka.bicKodi}
                type="text"
                placeholder="BIC Kodi"
                autoFocus
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>SWIFT Kodi<span style={{ color: "red" }}>*</span></Form.Label>
              <Form.Control
                onChange={handleChange('swiftKodi')}
                value={banka.swiftKodi}
                type="text"
                placeholder="SWIFT Kodi"
                autoFocus
              />
            </Form.Group>
            <Form.Group
              className="mb-3"
              controlId="exampleForm.ControlTextarea1"
            >
              <Form.Label>Valuta<span style={{ color: "red" }}>*</span></Form.Label>
              <select
                placeholder="Valuta"
                className="form-select"
                value={banka.valuta}
                onChange={(e) => handleValutaChange(e.target.value)}
              >
                <option defaultValue selected value="Euro">
                  Euro - €
                </option>
                <option value="Dollar">
                  Dollar - $
                </option>
                <option value="Franga Zvicerane">
                  Franga Zvicerane - CHF
                </option>
              </select>
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>Kodi IBAN<span style={{ color: "red" }}>*</span></Form.Label>
              <Form.Control
                onChange={handleChange('ibanFillimi')}
                value={banka.ibanFillimi}
                type="text"
                placeholder="XK05"
                autoFocus
              />
            </Form.Group>
            <Form.Group
              className="mb-3"
              controlId="exampleForm.ControlTextarea1"
            >
              <Form.Label>Lloji Bankes<span style={{ color: "red" }}>*</span></Form.Label>
              <select
                placeholder="Lloji Bankes"
                className="form-select"
                value={banka.llojiBankes}
                onChange={(e) => handleLlojiBankesChange(e.target.value)}
              >
                <option defaultValue selected value="Hyrese/Dalese">
                Hyrese/Dalese
                </option>
                <option value="Hyrese">
                  Hyrese
                </option>
                <option value="Dalese">
                  Dalese
                </option>
              </select>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={props.hide}>
            Anulo <FontAwesomeIcon icon={faXmark} />
          </Button>
          <Button
            style={{ backgroundColor: "#009879", border: "none" }}
            onClick={handleKontrolli}
          >
            Edito Banken <FontAwesomeIcon icon={faPenToSquare} />
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  )
}

export default EditoBanken;