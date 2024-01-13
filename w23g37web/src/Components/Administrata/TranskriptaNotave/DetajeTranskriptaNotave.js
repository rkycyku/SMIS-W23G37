import HeaderTranskriptaNotave from "./HeaderTranskriptaNotave";
import TeDhenatTranskriptaNotave from "./TeDhenatTranskriptaNotave";

function DetajeTranskriptaNotave(props) {
  return (
    <>
    {!props.LargoHeader &&
      <HeaderTranskriptaNotave
        id={props.id}
        Barkodi={props.Barkodi}
        NrFaqes={props.NrFaqes}
        NrFaqeve={props.NrFaqeve}
      />
    }
    {props.LargoHeader &&
    <br/>
    }
      <hr
        style={{
          height: "2px",
          borderWidth: "0",
          color: "gray",
          backgroundColor: "black",
        }}
      />
      <TeDhenatTranskriptaNotave
        id={props.id}
        ProduktiPare={props.ProduktiPare}
        ProduktiFundit={props.ProduktiFundit}
        LargoMesatarenKredit={props.LargoHeader}
      />
      <hr />
    </>
  );
}

export default DetajeTranskriptaNotave;
