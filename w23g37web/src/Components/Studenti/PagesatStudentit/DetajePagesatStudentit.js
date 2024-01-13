import HeaderPagesatStudentit from "./HeaderPagesatStudentit";
import TeDhenatPagesatStudentit from "./TeDhenatPagesatStudentit";
function DetajePagesatStudentit(props) {
  return (
    <>
      {!props.LargoHeader && (
        <HeaderPagesatStudentit
          id={props.id}
          Barkodi={props.Barkodi}
          NrFaqes={props.NrFaqes}
          NrFaqeve={props.NrFaqeve}
        />
      )}
      {props.LargoHeader && <br />}
      <hr
        style={{
          height: "2px",
          borderWidth: "0",
          color: "gray",
          backgroundColor: "black",
        }}
      />
      <TeDhenatPagesatStudentit
        id={props.id}
        ProduktiPare={props.ProduktiPare}
        ProduktiFundit={props.ProduktiFundit}
        LargoTotalin={props.LargoTotalin}
      />
      <hr />
    </>
  );
}

export default DetajePagesatStudentit;
