import { useRef } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import axios from "axios";
import { useNavigate } from "react-router-dom";
 
function AddSupProduct() {
  const Name = useRef("");
  const price = useRef("");
  const detail = useRef("");
  const imgUrl = useRef("");
 
  const navigate = useNavigate();
 
  function addProductHandler() {
    var payload = {
      Name: Name.current.value,
      price: price.current.value,
      detail: detail.current.value,
      imageUrl: imgUrl.current.value,
    };
 
    axios
      .post("https://localhost:7051/api/Products", payload)
      .then((response) => {
        navigate("/");
      });
  }
 
  return (
    <>
      <legend>Add A New Products Character</legend>
      <form>
        <Form.Group className="mb-3" controlId="formProductName">
          <Form.Label>Product Name</Form.Label>
          <Form.Control type="text" ref={Name} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formprice">
          <Form.Label>price</Form.Label>
          <Form.Control type="text" ref={price} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formdetail">
          <Form.Label>detail</Form.Label>
          <Form.Control as="textarea" rows={3} ref={detail} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formImgUrl">
          <Form.Label>Image URL</Form.Label>
          <Form.Control type="text" ref={imgUrl} />
        </Form.Group>
      </form>
      <Button variant="primary" type="button" onClick={addProductHandler}>
        Submit
      </Button>
    </>
  );
}
export default AddSupProduct;