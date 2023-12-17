import { React, useState, useRef } from "react";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import Modal from "react-bootstrap/Modal";
import axios from "axios";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faXmark, faBan, faL, faAdd } from "@fortawesome/free-solid-svg-icons";
import { useEffect } from "react";
import Col from "react-bootstrap/esm/Col";
import useKeyboardNavigation from "../../Context/useKeyboardNavigation";
import DatePicker from "react-datepicker";

const ShtoProduktin = (props) => {
  const [bankaID, setBankaID] = useState("");
  const [personiID, setPersoniID] = useState(null);
  const [aplikimiID, setAplikimiID] = useState(null);
  const [pagesa, setPagesa] = useState(null);
  const [faturimi, setFaturimi] = useState(null);
  const [llojiPageses, setLlojiPageses] = useState("Hyrese");
  const [arsyejaEPageses, setArsyejaEPageses] = useState(null);
  const [pershkrimiPageses, setPershkrimiPageses] = useState("");
  const [dataPageses, setDataPageses] = useState(null);

  const [perditeso, setPerditeso] = useState("");

  const [bankat, setBankat] = useState([]);
  const [personat, setPersonat] = useState([]);
  const [fushatEZbrazura, setFushatEZbrazura] = useState(false);

  const [inputPersonave, setInputPersonave] = useState("");
  const [filtrimiPersonave, setFiltrimiPersonave] = useState(personat);
  const produktiZgjedhur = useKeyboardNavigation(filtrimiPersonave);

  useEffect(() => {
    const vendosTeDhenat = async () => {
      try {
        const bankat = await axios.get(
          `https://localhost:7251/api/Financat/ShfaqBankat`,
          authentikimi
        );
        const personat = await axios.get(
          `https://localhost:7251/api/Financat/ShfaqniPersonatPerPagese`,
          authentikimi
        );
        setBankat(bankat.data);
        setPersonat(personat.data);
      } catch (err) {
        console.log(err);
      }
    };

    vendosTeDhenat();
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

  const handleBankaChange = (event) => {
    setBankaID(event);
  };

  const handleLlojiPagesesChange = (event) => {
    setLlojiPageses(event);
  };

  const handleArsyejaEPagesesChange = (event) => {
    setArsyejaEPageses(event);
  };

  function isNullOrEmpty(value) {
    return value === null || value === "" || value === undefined;
  }

  async function handleSubmit() {
    try {
      await axios
        .post(
          "https://localhost:7251/api/Financat/ShtoniPagesen",
          {
            pagesa: {
              bankaID: bankaID,
              aplikimiID: aplikimiID,
              personiID: personiID,
              pagesa: pagesa,
              faturimi: faturimi,
              pershkrimiPageses: `${arsyejaEPageses} - ${pershkrimiPageses}`,
              llojiPageses: llojiPageses,
              dataPageses: dataPageses,
            },
          },
          authentikimi
        )
        .then(async (response) => {
          props.setTipiMesazhit("success");
          props.setPershkrimiMesazhit("Pagesa u insertua me sukses!");
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
    if (
      isNullOrEmpty(bankaID) ||
      (isNullOrEmpty(pagesa) && isNullOrEmpty(faturimi))
    ) {
      setFushatEZbrazura(true);
    } else {
      handleSubmit();
    }
  };

  const handleInputChange = (e, kat) => {
    const tekstiPerFiltrim = e.target.value.toLowerCase();

    if (kat === "Personi") {
      setInputPersonave(tekstiPerFiltrim);

      const filtrimi = personat.filter((item) =>
        item.emri.toLowerCase().includes(tekstiPerFiltrim)
      );

      setFiltrimiPersonave(filtrimi);
    }
  };
  const handleInputKeyDown = (e, kat) => {
    if (e.key === "Enter") {
      e.preventDefault();

      if (kat === "Personi") {
        if (filtrimiPersonave.length > 0) {
          perditesoPersonin(filtrimiPersonave[produktiZgjedhur], kat);
        }

        ndrroField(e, "pagesa");
      }
    }
  };

  const perditesoPersonin = (e, kat) => {
    console.log(e);
    if (kat === "Personi") {
      if (e.llojiPersonit == "Perdorues") {
        setPersoniID(e.id);
        setAplikimiID(null);
      } else if (e.llojiPersonit == "AplimiIRi") {
        setAplikimiID(e.id);
        setPersoniID(null);
      } else {
        setPersoniID(e.id);
        setAplikimiID(e.id);
      }
      setInputPersonave(
        `${e.emri} ${e.mbiemri} - ${
          e.kodiFinanciar ? e.kodiFinanciar : e.username
        }`
      );
      setFiltrimiPersonave([]);
    }
  };

  const ndrroField = (e, tjetra) => {
    if (e.key === "Enter") {
      e.preventDefault();
      document.getElementById(tjetra).focus();
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
          <Modal.Title>Shtoni Pagesen</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group
              className="mb-3"
              controlId="exampleForm.ControlTextarea1">
              <Form.Label>
                Banka<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <select
                placeholder="Banka"
                className="form-select"
                value={bankaID}
                onChange={(e) => handleBankaChange(e.target.value)}>
                <option value={0} hidden selected>
                  Zgjedhni Banken
                </option>
                {bankat &&
                  bankat.map((banka) => (
                    <option value={banka.bankaID}>
                      {banka.emriBankes} - {banka.kodiBankes}
                    </option>
                  ))}
              </select>
            </Form.Group>
            <Form.Group as={Col} controlId="Personi" className="mb-3">
              <Form.Label>
                Personi<span style={{ color: "red" }}>*</span>
              </Form.Label>

              <Form.Control
                type="text"
                name="Personi"
                className="form-control styled-input"
                placeholder="Personi"
                value={inputPersonave}
                onChange={(e) => handleInputChange(e, "Personi")}
                onKeyDown={(e) => handleInputKeyDown(e, "Personi")}
                onFocus={(e) => handleInputChange(e, "Personi")}
                autoComplete={false}
              />

              <div className="container" style={{ position: "relative" }}>
                <ul className="list-group mt-2 searchBoxi">
                  {filtrimiPersonave.map((item, index) => (
                    <li
                      key={item.id}
                      className={`list-group-item${
                        produktiZgjedhur === index ? " active" : ""
                      }`}
                      onClick={() => perditesoPersonin(item, "Personi")}>
                      {item.emri} {item.mbiemri} -{" "}
                      {item.kodiFinanciar ? item.kodiFinanciar : item.username}
                    </li>
                  ))}
                </ul>
              </div>
            </Form.Group>
            <Form.Group
              className="mb-3"
              controlId="exampleForm.ControlTextarea1">
              <Form.Label>
                Lloji Pageses<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <select
                placeholder="Lloji Pageses"
                className="form-select"
                value={llojiPageses}
                onChange={(e) => handleLlojiPagesesChange(e.target.value)}>
                <option defaultValue selected value="Hyrese">
                  Hyrese
                </option>
                <option value="Dalese">Dalese</option>
              </select>
            </Form.Group>
            <Form.Group
              className="mb-3"
              controlId="exampleForm.ControlTextarea1">
              <Form.Label>
                Arsyeja Pageses<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <select
                placeholder="Lloji Pageses"
                className="form-select"
                value={arsyejaEPageses}
                onChange={(e) => handleArsyejaEPagesesChange(e.target.value)}>
                <option defaultValue selected disabled value={null}>
                  Zgjedhni Arsyjen e Pageses{" "}
                </option>
                {llojiPageses == "Hyrese" && (
                  <option value="Pagese nga Studenti">
                    Pagese nga Studenti
                  </option>
                )}
                {llojiPageses == "Dalese" && (
                  <>
                    <option value="Pagese e Pagave">Pagese e Pagave</option>
                    <option value="Pagese e Faturave">Pagese e Faturave</option>
                    <option value="Pagese e Shpenzimeve per Materiale Shkollore">
                      Pagese e Shpenzimeve per Materiale Shkollore
                    </option>
                    <option value="Pagese e Faturave">
                      Pagese e Licensave te Aplikacioneve
                    </option>
                  </>
                )}
              </select>
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>
                Pagesa<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                onChange={handleChange(setPagesa)}
                value={pagesa}
                type="text"
                placeholder="Pagesa"
                id="pagesa"
                disabled={llojiPageses == "Dalese" ? true : false}
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>
                Faturimi<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Form.Control
                onChange={handleChange(setFaturimi)}
                value={faturimi}
                type="text"
                placeholder="Faturimi"
                disabled={llojiPageses == "Hyrese" ? true : false}
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>Pershkrimi Pageses</Form.Label>
              <Form.Control
                onChange={handleChange(setPershkrimiPageses)}
                value={pershkrimiPageses}
                as="textarea"
                placeholder="Pershkrimi Pageses"
              />
            </Form.Group>
            <Form.Group as={Col} className="p-0" controlId="formGridLastName">
              <Form.Label>
                Data Pageses<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <Col>
                <DatePicker
                  selected={dataPageses}
                  onChange={(date) => setDataPageses(date)}
                  className="form-control"
                  dateFormat="dd/MM/yyyy"
                />
              </Col>
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
            Ruaj Pagesen <FontAwesomeIcon icon={faAdd} />
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

export default ShtoProduktin;
