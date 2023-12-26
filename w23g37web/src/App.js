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
import Statistika from "./Pages/Financat/Statistika";

function App() {
  const { setTitle, aktivizoSetPerditeso } = useTitle();
  const [perditeso, setPerditeso] = useState(Date.now());
  const navigate = useNavigate();

  useEffect(() => {
    setPerditeso(Date.now());
  }, [navigate]);

  useEffect(() => {
    setTitle(document.title.split("|")[0].trim());
    aktivizoSetPerditeso();
  }, [perditeso]);

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
        <Route path="/Statistika" element={<Statistika />} />
        <Route
          path="/RegjistrimiISemestrit"
          element={<RegjistrimiISemestrit />}
        />
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </div>
  );
}

export default App;
