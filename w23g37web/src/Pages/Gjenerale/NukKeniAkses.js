import axios from "axios";
import { useState, useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import jsPDF from "jspdf";
import html2canvas from "html2canvas";
import { Table } from "react-bootstrap";
import { MDBBtn } from "mdb-react-ui-kit";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faDownload, faArrowLeft } from "@fortawesome/free-solid-svg-icons";
import { Helmet } from "react-helmet";
import "../Styles/ProductTables.css";

function NukKeniAkses(props) {
  return (
    <>
      <div className="containerDashboardP">
        <Helmet>
          <title>Nuk Keni Akses | UBT SMIS</title>
        </Helmet>
        <div className="teDhenatAplikimit">
          <div className="teDhenatAplikimitHeader">
            <img
              src={`${process.env.PUBLIC_URL}/Ubtlogo.png`}
              style={{ width: "100px", height: "auto", marginTop: "0.5em" }}
            />
            <h4>Universiteti për Biznes dhe Teknologji</h4>
            <h1 style={{marginTop: "2em"}}>403 - Nuk keni akses per kete pjese</h1>
          </div>
          
        </div>
      </div>
    </>
  );
}

export default NukKeniAkses;
