import { useState, useRef, useEffect } from "react";
import axios from "axios";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import Modal from "react-bootstrap/Modal";
import { InputGroup } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faXmark } from "@fortawesome/free-solid-svg-icons";
import { faPenToSquare } from "@fortawesome/free-solid-svg-icons";

function EditoTarifen(props) {
  const [tarifa, setTarifa] = useState([]);

  const [perditeso, setPerditeso] = useState("");
  const [fushatEZbrazura, setFushatEZbrazura] = useState(false);

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    const shfaqTarifen = async () => {
      try {
        const tarifa = await axios.get(
          `https://localhost:7251/api/Financat/ShfaqniDetajetTarifes?TarifaID=` +
            props.id,
          authentikimi
        );
        setTarifa(tarifa.data);
      } catch (err) {
        console.log(err);
      }
    };

    shfaqTarifen();
  }, []);

  const handleChange = (propertyName) => (event) => {
    setTarifa((prev) => ({
      ...prev,
      [propertyName]: event.target.value,
    }));
  };

  function isNullOrEmpty(value) {
    return value === null || value === "" || value === undefined;
  }

  async function handleSubmit() {
    try {
      await axios
        .put(
          `https://localhost:7251/api/Financat/PerditesoniTarifenDepartamentit?tarifaID=` +
            props.id,
          {
            tarifatDepartamenti: {
              tarifaVjetore: tarifa.tarifaVjetore,
            },
          },
          authentikimi
        )
        .then((x) => {
          props.setTipiMesazhit("success");
          props.setPershkrimiMesazhit("Tarifa u Perditesua me sukses!");
          props.perditesoTeDhenat();
          props.hide();
          props.shfaqmesazhin();
        })
        .catch((error) => {
          console.error("Error saving tarifa:", error);
          props.setTipiMesazhit("danger");
          props.setPershkrimiMesazhit(
            "Ndodhi nje gabim gjate perditesimit te tarifes!"
          );
          props.perditesoTeDhenat();
          props.shfaqmesazhin();
        });
    } catch (error) {
      console.error(error);
    }
  }

  const handleKontrolli = () => {
    if (isNullOrEmpty(tarifa.tarifaVjetore)) {
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
          <Modal.Title>Edito Tarifen</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>Niveli Studimit</Form.Label>
              <Form.Control
                value={(tarifa.niveliStudimeve && tarifa.niveliStudimeve.emriNivelitStudimeve) + " - " + (tarifa.niveliStudimeve && tarifa.niveliStudimeve.shkurtesaEmritNivelitStudimeve)}
                type="text"
                placeholder="Niveli Studimit"
                autoFocus
                disabled
              />
            </Form.Group>

            <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
              <Form.Label>
                Tarifa Vjetore<span style={{ color: "red" }}>*</span>
              </Form.Label>
              <InputGroup className="mb-3">
                <Form.Control
                  onChange={handleChange("tarifaVjetore")}
                  value={tarifa.tarifaVjetore}
                  type="number"
                  placeholder="Tarifa Vjetore"
                  autoFocus
                />
                <InputGroup.Text id="basic-addon1">€</InputGroup.Text>
              </InputGroup>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={props.hide}>
            Anulo <FontAwesomeIcon icon={faXmark} />
          </Button>
          <Button
            style={{ backgroundColor: "#009879", border: "none" }}
            onClick={handleKontrolli}>
            Edito Tarifen <FontAwesomeIcon icon={faPenToSquare} />
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default EditoTarifen;
