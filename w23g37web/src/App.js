import { Navigate, useNavigate, Route, Routes } from "react-router-dom";
import "./App.css";
import Dashboard from "./Pages/Dashboard";
import LogIn from "./Pages/LogIn";
import PerditesoTeDhenat from "./Pages/Gjenerale/TeDhenat/PerditesoTeDhenat";
import { useTitle } from "./Context/TitleProvider";
import { useEffect } from "react";
import { useState } from "react";
import KrijoAplikiminERi from "./Pages/Administrata/KrijoAplikiminERi";
import Bankat from "./Pages/Financat/Bankat";
import Pagesat from "./Pages/Financat/Pagesat";
import RegjistrimiISemestrit from "./Pages/Studenti/RegjistrimiISemestrit";
import TarifatDepartamentit from "./Pages/Financat/TarifatDepartamentit";
import AplikimetEReja from "./Pages/Administrata/AplikimetEReja";
import ListaStudenteve from "./Pages/Administrata/ListaStudenteve";
import ParaqitjaProvimit from "./Pages/Studenti/ParaqitjaProvimit";
import ProvimetEParaqitura from "./Pages/Studenti/ProvimetEParaqitura";
import VendosNotat from "./Pages/Profesor/VendosNotat";
import TranskriptaNotave from "./Pages/Studenti/TranskriptaNotave";
import PagesatEStudentit from "./Pages/Studenti/PagesatEStudentit";
import TarifatStudenti from "./Pages/Financat/TarifatStudenti";
import PagesatEPaPerfunduara from "./Pages/Studenti/PagesatEPaPerfunduara";
import NukKeniAkses from "./Pages/Gjenerale/NukKeniAkses";
import axios from "axios";

function App() {
  const { setTitle, aktivizoSetPerditeso } = useTitle();
  const [perditeso, setPerditeso] = useState(Date.now());
  const navigate = useNavigate();

  const getID = localStorage.getItem("id");

  const getToken = localStorage.getItem("token");

  const authentikimi = {
    headers: {
      Authorization: `Bearer ${getToken}`,
    },
  };

  useEffect(() => {
    setPerditeso(Date.now());
  }, [navigate]);

  useEffect(() => {
    setTitle(document.title.split("|")[0].trim());
    aktivizoSetPerditeso();
  }, [perditeso]);

  useEffect(() => {
    if (getID) {
      const vendosTeDhenat = async () => {
        try {
          const rolet = await axios.get(
            `https://localhost:7251/api/Perdoruesi/shfaqSipasID?idUserAspNet=${getID}`,
            authentikimi
          );

          if (rolet.data.rolet.includes("Student")) {
            await axios.post(
              `https://localhost:7251/api/Studentet/VendosTarifenMujore?studentiID=${getID}`,
              authentikimi
            );
          }
        } catch (err) {
          console.log(err);
        }
      };

      vendosTeDhenat();
    } else {
      navigate("/login");
    }
  }, []);

  return (
    <div className="App">
      <Routes>
        <Route exact path="/" element={<Dashboard />} />
        <Route path="/Dashboard" element={<Dashboard key={Date.now()} />} />
        <Route path="/LogIn" element={<LogIn />} />
        <Route path="/PerditesoTeDhenat" element={<PerditesoTeDhenat />} />
        <Route path="/KrijoAplikiminERi" element={<KrijoAplikiminERi />} />
        <Route path="/Bankat" element={<Bankat />} />
        <Route path="/Pagesat" element={<Pagesat />} />
        <Route
          path="/TarifatDepartamentit"
          element={<TarifatDepartamentit />}
        />
        <Route path="/AplikimetEReja" element={<AplikimetEReja />} />
        <Route path="/ListaStudenteve" element={<ListaStudenteve />} />
        <Route path="/ParaqitjaProvimit" element={<ParaqitjaProvimit />} />
        <Route path="/ProvimetEParaqitura" element={<ProvimetEParaqitura />} />
        <Route path="/VendosNotat" element={<VendosNotat />} />
        <Route path="/TranskriptaNotave" element={<TranskriptaNotave />} />
        <Route path="/PagesatEStudentit" element={<PagesatEStudentit />} />
        <Route
          path="/RegjistrimiISemestrit"
          element={<RegjistrimiISemestrit />}
        />
        <Route path="/TarifatStudenti" element={<TarifatStudenti />} />
        <Route
          path="/PagesatEPaPerfunduara"
          element={<PagesatEPaPerfunduara />}
        />
        <Route path="/NukKeniAkses" element={<NukKeniAkses />} />
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </div>
  );
}

export default App;
