// Bootstrap CSS
import "bootstrap/dist/css/bootstrap.min.css";
// Bootstrap Bundle JS
import "bootstrap/dist/js/bootstrap.bundle.min";
import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.js";
import { BrowserRouter } from "react-router-dom";
import { Helmet } from "react-helmet";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircleUser } from "@fortawesome/free-regular-svg-icons";
import {
  faRightFromBracket,
  faRightToBracket,
  faCartShopping,
  faBars,
  faXmark,
  faAddressBook,
  faLock,
  faBuilding,
  faBook,
  faTimeline,
  faChevronUp,
  faPenToSquare,
  faBuildingColumns,
  faReceipt,
  faChartPie,
  faEuroSign,
} from "@fortawesome/free-solid-svg-icons";
import { faUserPlus } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";
import jwtDecode from "jwt-decode";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import axios from "axios";
import "./NavBar.css";
import { Bars } from "react-loader-spinner";
import { useLocation } from "react-router-dom";
import { useTitle } from "./Context/TitleProvider.js";

function Navbar(props) {
  const navigate = useNavigate();
  const location = useLocation();
  const token = localStorage.getItem("token");
  const [teDhenatBiznesit, setTeDhenatBiznesit] = useState([]);
  const [perditeso, setPerditeso] = useState("");

  const [teDhenat, setTeDhenat] = useState([]);

  const [showMenu, setShowMenu] = useState(false);
  const [showMenu2, setShowMenu2] = useState(false);
  const [isSidebarOpen, setIsSidebarOpen] = useState(true);

  const getID = localStorage.getItem("id");
  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    if (getID) {
      const vendosTeDhenat = async () => {
        try {
          const perdoruesi = await axios.get(
            `https://localhost:7251/api/Perdoruesi/shfaqSipasID?idUserAspNet=${getID}`,
            authentikimi
          );
          setTeDhenat(perdoruesi.data);
          console.log(teDhenat.data);
        } catch (err) {
          console.log(err);
        }
      };

      vendosTeDhenat();
    } else {
      navigate("/login");
    }
  }, [perditeso]);

  useEffect(() => {
    if (token) {
      const decodedToken = jwtDecode(token);
      const kohaAktive = new Date(decodedToken.exp * 1000);
      const kohaTanishme = new Date();
      const id = localStorage.getItem("id");

      if (kohaAktive < kohaTanishme) {
        localStorage.removeItem("token");
        localStorage.removeItem("id");
        navigate("/LogIn");
      }

      if (id !== decodedToken.id) {
        localStorage.removeItem("token");
        localStorage.removeItem("id");
        navigate("/LogIn");
      }
    }
  }, []);

  const handleSignOut = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("id");
  };

  const handleArrowClick = (e) => {
    const arrowParent = e.currentTarget.parentElement.parentElement;
    setShowMenu(!showMenu);
    arrowParent.classList.toggle("showMenu");
  };

  const handleAnchorClick = (e) => {
    e.preventDefault();
    const anchorElementsParent =
      e.currentTarget.parentElement.parentElement.parentElement;

    document.querySelectorAll(".showMenu2").forEach((element) => {
      if (element !== anchorElementsParent) {
        element.classList.remove("open");
        element.classList.remove("showMenu2");
      }
    });

    setShowMenu2(!showMenu2);
    anchorElementsParent.classList.toggle("showMenu2");
    anchorElementsParent.classList.toggle("open");
  };

  const handleSidebarClick = () => {
    setIsSidebarOpen(!isSidebarOpen);
  };

  const { title, emri, rolet, mbiemri } = useTitle();

  return (
    <>
      {token && (
        <>
          <div className={isSidebarOpen ? "sidebar close" : "sidebar"}>
            <div class="logo-details">
              <Link to="/">
                <img
                  class="logoImg"
                  src={process.env.PUBLIC_URL + "/Ubtlogo.png"}
                />
              </Link>
              <span class="logo_name">SMIS Admin</span>
            </div>
            <ul class="nav-links">
              {/* <li>
                <div class="iocn-link">
                  <Link to="#">
                    <i>
                      <FontAwesomeIcon icon={faBook} />
                    </i>
                    <span class="link_name">Lendet</span>
                  </Link>
                  <i
                    class="bx bxs-chevron-down arrow"
                    onClick={handleArrowClick}>
                    <FontAwesomeIcon icon={faChevronUp} />
                  </i>
                </div>
                <ul class="sub-menu">
                  <li>
                    <Link class="link_name" to="#">
                      Lendet
                    </Link>
                  </li>
                  <li>
                    <Link to="#">Menaxhoni Lendet</Link>
                  </li>
                </ul>
              </li> */}
              {/* <li>
                <div class="iocn-link">
                  <Link to="#">
                    <i>
                      <FontAwesomeIcon icon={faTimeline} />
                    </i>
                    <span class="link_name">Afatet</span>
                  </Link>
                  <i
                    class="bx bxs-chevron-down arrow"
                    onClick={handleArrowClick}>
                    <FontAwesomeIcon icon={faChevronUp} />
                  </i>
                </div>
                <ul class="sub-menu">
                  <li>
                    <Link class="link_name" to="#">
                      Afatet
                    </Link>
                  </li>

                  <li>
                    <div class="icon-link2">
                      <Link to="#">
                        Te Ndryshme{" "}
                        <i
                          class="bx bxs-chevron-down arrow2"
                          onClick={handleAnchorClick}>
                          <FontAwesomeIcon icon={faChevronUp} />
                        </i>
                      </Link>
                    </div>
                    <ul class="sub-menu2">
                      <li>
                        <Link class="link_name" to="#">
                          Te Ndryshme
                        </Link>
                      </li>
                      <li>
                        <Link to="#">Niveli Studimeve</Link>
                      </li>
                    </ul>
                  </li>
                </ul>
              </li> */}
              {teDhenat &&
                teDhenat.rolet &&
                teDhenat.rolet.includes("Financa") && (
                  <>
                    <li>
                      <Link to="/Bankat">
                        <i class="bx bx-pie-chart-alt-2">
                          <FontAwesomeIcon icon={faBuildingColumns} />
                        </i>
                        <span class="link_name">Bankat</span>
                      </Link>
                      <ul class="sub-menu blank">
                        <li>
                          <Link class="link_name" to="/Bankat">
                            Bankat
                          </Link>
                        </li>
                      </ul>
                    </li>
                    <li>
                      <Link to="/Pagesat">
                        <i class="bx bx-pie-chart-alt-2">
                          <FontAwesomeIcon icon={faReceipt} />
                        </i>
                        <span class="link_name">Pagesat</span>
                      </Link>
                      <ul class="sub-menu blank">
                        <li>
                          <Link class="link_name" to="/Pagesat">
                            Pagesat
                          </Link>
                        </li>
                      </ul>
                    </li>
                    <li>
                      <Link to="/TarifatDepartamentit">
                        <i class="bx bx-pie-chart-alt-2">
                          <FontAwesomeIcon icon={faEuroSign} />
                        </i>
                        <span class="link_name">Tarifat e Departamentit</span>
                      </Link>
                      <ul class="sub-menu blank">
                        <li>
                          <Link class="link_name" to="/TarifatDepartamentit">
                            Tarifat e Departamentit
                          </Link>
                        </li>
                      </ul>
                    </li>
                    <li>
                      <Link to="/Statistika">
                        <i class="bx bx-pie-chart-alt-2">
                          <FontAwesomeIcon icon={faChartPie} />
                        </i>
                        <span class="link_name">Statistika</span>
                      </Link>
                      <ul class="sub-menu blank">
                        <li>
                          <Link class="link_name" to="/Statistika">
                            Statistika
                          </Link>
                        </li>
                      </ul>
                    </li>
                  </>
                )}
              {teDhenat &&
                teDhenat.rolet &&
                teDhenat.rolet.includes("Administrat") && (
                  <>
                    <li>
                      <div class="iocn-link">
                        <Link to="#">
                          <i>
                            <FontAwesomeIcon icon={faBook} />
                          </i>
                          <span class="link_name">Aplikimet e Reja</span>
                        </Link>
                        <i
                          class="bx bxs-chevron-down arrow"
                          onClick={handleArrowClick}>
                          <FontAwesomeIcon icon={faChevronUp} />
                        </i>
                      </div>
                      <ul class="sub-menu">
                        <li>
                          <Link class="link_name" to="#">
                          Aplikimet e Reja
                          </Link>
                        </li>
                        <li>
                          <Link to="/AplikimetEReja">Lista Aplikimeve</Link>
                        </li>
                        <li>
                          <Link to="/KrijoAplikiminERi">Krijoni Aplikim e Ri</Link>
                        </li>
                      </ul>
                    </li>
                  </>
                )}
              {teDhenat &&
                teDhenat.rolet &&
                teDhenat.rolet.includes("Student") && (
                  <>
                    <li>
                      <Link to="/RegjistrimiISemestrit">
                        <i class="bx bx-pie-chart-alt-2">
                          <FontAwesomeIcon icon={faBuildingColumns} />
                        </i>
                        <span class="link_name">Regjistrimi i Semestrit</span>
                      </Link>
                      <ul class="sub-menu blank">
                        <li>
                          <Link class="link_name" to="/RegjistrimiISemestrit">
                            Regjistrimi i Semestrit
                          </Link>
                        </li>
                      </ul>
                    </li>
                  </>
                )}
              <li>
                <Link to="/PerditesoTeDhenat">
                  <i class="bx bx-pie-chart-alt-2">
                    <FontAwesomeIcon icon={faPenToSquare} />
                  </i>
                  <span class="link_name">Perditesimi i Fjalekalimit</span>
                </Link>
                <ul class="sub-menu blank">
                  <li>
                    <Link class="link_name" to="/PerditesoTeDhenat">
                      Perditesimi i Fjalekalimit
                    </Link>
                  </li>
                </ul>
              </li>
              <li>
                <div class="profile-details">
                  <div class="profile-content"></div>

                  <a>
                    <div class="name-job">
                      {teDhenat && (
                        <>
                          <div class="profile_name">{emri + " " + mbiemri}</div>

                          <div class="job">{rolet.join(", ")}</div>
                        </>
                      )}
                    </div>
                  </a>
                  <Link to="/" onClick={handleSignOut}>
                    <i class="bx bx-log-out">
                      <FontAwesomeIcon icon={faRightFromBracket} />
                    </i>
                  </Link>
                </div>
              </li>
            </ul>
          </div>
          <section class="home-section">
            <div class="home-content">
              <i class="bx bx-menu" onClick={handleSidebarClick}>
                <FontAwesomeIcon icon={isSidebarOpen ? faBars : faXmark} />
              </i>

              <span class="text"> {title}</span>
            </div>
            <div class="col py-3 ms-3">
              <App />
            </div>
          </section>
        </>
      )}
      {!token && <App />}
    </>
  );
}

export default Navbar;
