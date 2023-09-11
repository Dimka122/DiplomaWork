import Button from "react-bootstrap/Button";
import axios from "axios";
import { useNavigate, useParams } from "react-router-dom";
 
function UpdateSuperVillain() {
  const superVillainName = useRef("");
  const franchise = useRef("");
  const powers = useRef("");
  const imgUrl = useRef("");
 
  const navigate = useNavigate();
 
  const { id } = useParams();
 
  useEffect(() => {
    axios.get(`https://localhost:7273/SuperVillain/${id}`).then((response) => {
      superVillainName.current.value = response.data.villainName;
      franchise.current.value = response.data.franchise;
      powers.current.value = response.data.powers;
      imgUrl.current.value = response.data.imageUrl;
    });
  }, []);
 
  function updateVillainHandler() {
    var payload = {
      villainName: superVillainName.current.value,
      powers: powers.current.value,
      franchise: franchise.current.value,
      imageUrl: imgUrl.current.value,
      id: id,
    };
    axios
      .put(`https://localhost:7273/SuperVillain/`, payload)
      .then((response) => {
        navigate("/");
      });
  }
 
  return (
    <>
      <legend>Update SuperVilian</legend>
      <form>
        <Form.Group className="mb-3" controlId="formSuperVillainName">
          <Form.Label>Super Villain Name</Form.Label>
          <Form.Control type="text" ref={superVillainName} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formFranchise">
          <Form.Label>Franchise</Form.Label>
          <Form.Control type="text" ref={franchise} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formPowers">
          <Form.Label>Powers</Form.Label>
          <Form.Control as="textarea" rows={3} ref={powers} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formImgUrl">
          <Form.Label>Image URL</Form.Label>
          <Form.Control type="text" ref={imgUrl} />
        </Form.Group>
      </form>
      <Button variant="primary" type="button" onClick={updateVillainHandler}>
        Submit
      </Button>
    </>
  );
}
export default UpdateSuperV;