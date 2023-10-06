import { useRef } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import axios from "axios";
import { useNavigate } from "react-router-dom";
 
function AddProduct() {
  const Name = useRef("");
  const category = useRef("");
  const detail = useRef("");
  const imgUrl = useRef("");
 
  const navigate = useNavigate();
 
  function addProductHandler() {
    var payload = {
      Name: Name.current.value,
      category: category.current.value,
      detail: detail.current.value,
      imageUrl: imgUrl.current.value,
    };
 
    axios
      .post("https://localhost:7051/api/Products/AddProduct", payload)
      .then((response) => {
        navigate("/");
      });
  }
 
  return (
    <>
      <legend>Add A New Products</legend>
      <form>
        <Form.Group className="mb-3" controlId="formName">
          <Form.Label>Name</Form.Label>
          <Form.Control type="text" ref={Name} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formcategory">
          <Form.Label>Category</Form.Label>
          <Form.Control type="text" ref={category} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formdetail">
          <Form.Label>Detail</Form.Label>
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
export default AddProduct;