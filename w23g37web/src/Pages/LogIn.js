import React from "react";

import { useLocation } from "react-router-dom";
import { Helmet } from "react-helmet";
import Form from "react-bootstrap/Form";
import "./Styles/LogIn.css";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import axios from "axios";
import jwt_decode from "jwt-decode";
import {
  MDBContainer,
  MDBRow,
  MDBCol,
  MDBCard,
  MDBCardBody,
  MDBInput,
} from "mdb-react-ui-kit";
import { Link } from "react-router-dom";
import Mesazhi from "../Components/TeTjera/layout/Mesazhi";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faRightToBracket } from "@fortawesome/free-solid-svg-icons";

const LogIn = () => {
  const navigate = useNavigate();

  const location = useLocation();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loggedIn, setLoggedIn] = useState(false);

  const [shfaqMesazhin, setShfaqMesazhin] = useState(false);
  const [tipiMesazhit, setTipiMesazhit] = useState("");
  const [pershkrimiMesazhit, setPershkrimiMesazhit] = useState("");

  const getToken = localStorage.getItem("token");

  useEffect(() => {
    const searchParams = new URLSearchParams(location.search);
    const token = searchParams.get("token");

    if (token) {
      localStorage.setItem("token", token);

      const decodedToken = jwt_decode(token);

      localStorage.setItem("id", decodedToken.id);

      setTimeout(() => {
        navigate("/dashboard");
        window.location.reload();
      }, 200);
    }
  }, [location.search]);

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  function vendosEmail(value) {
    setEmail(value);
  }

  function vendosPasswordin(value) {
    setPassword(value);
  }

  useEffect(() => {
    const loggedUser = localStorage.getItem("user");

    if (loggedUser) {
      setLoggedIn(true);
    }
  });

  async function handleLogIn(e) {
    e.preventDefault();

    try {
      const response = await axios.post(
        "https://localhost:7251/api/Authenticate/login",
        {
          email: email,
          password: password,
        },
        authentikimi
      );

      if (response.status === 200) {
        const { token } = response.data;

        localStorage.setItem("token", token);

        const decodedToken = jwt_decode(token);

        localStorage.setItem("id", decodedToken.id);

        navigate("/dashboard");
      } else {
        setPershkrimiMesazhit(
          "<strong>Ju lutemi kontaktoni me stafin pasi ndodhi nje gabim ne server!</strong>"
        );
        setTipiMesazhit("danger");
        setShfaqMesazhin(true);
      }
    } catch (error) {
      setPershkrimiMesazhit(
        "<strong>Ju lutemi kontrolloni te dhenat e juaja!</strong>"
      );
      setTipiMesazhit("danger");
      setShfaqMesazhin(true);

      console.log(error);
    }
  }

  return (
    <>
      <Helmet>
        <title>Log In | UBT SMIS</title>
      </Helmet>
      <div class="login">
        {shfaqMesazhin && (
          <Mesazhi
            setShfaqMesazhin={setShfaqMesazhin}
            pershkrimi={pershkrimiMesazhit}
            tipi={tipiMesazhit}
          />
        )}
        <img class="loginImg" src={process.env.PUBLIC_URL + "/Ubtlogo.png"} />
        <h1>{document.title}</h1>
        <div class="row">
          <section>
            <form id="account" method="post">
              <hr />
              <MDBInput
                wrapperClass="mb-4 w-100"
                label="Email address"
                id="formControlEmailAddress"
                type="email"
                size="lg"
                onChange={(e) => vendosEmail(e.target.value)}
              />
              <MDBInput
                wrapperClass="mb-4 w-100"
                label="Password"
                id="formControlPassword"
                type="password"
                size="lg"
                onChange={(e) => vendosPasswordin(e.target.value)}
              />
              <div>
                <button
                  class="w-100 btn btn-lg btn-primary"
                  role="button"
                  onClick={handleLogIn}>
                  Sign in <FontAwesomeIcon icon={faRightToBracket} />
                </button>
              </div>
              <div>
                <hr />
                <p>
                  Kontaktoni me stafin per{" "}
                  <a href="mailto:support@ubt-uni.net"> support</a> ne rast se
                  keni harruar fjalekalimin!
                </p>
              </div>
            </form>
          </section>
        </div>
      </div>
    </>
  );
};

export default LogIn;
