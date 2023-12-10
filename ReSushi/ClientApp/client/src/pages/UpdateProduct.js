import { useRef, useEffect } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import axios from "axios";
import { useNavigate, useParams } from "react-router-dom";

function UpdateProduct() {

    const Name = useRef("");
    const category = useRef("");
    const detail = useRef("");
    const imgUrl = useRef("");
   
    const navigate = useNavigate();
   
    const { id } = useParams();
   
    useEffect(() => {
      axios.get(`https://localhost:7051/api/Products/${id}`).then((response) => {
        //id:id;
        Name.current.value = response.data.Name;
        category.current.value = response.data.category;
        detail.current.value = response.data.detail;
        imgUrl.current.value = response.data.imageUrl;
        
      });
    }, [id]);
   
    function updateProductHandler() {
      var payload = {
        Name: Name.current.value,
      category: category.current.value,
      detail: detail.current.value,
      imageUrl: imgUrl.current.value,
       // id: id.current.value,
      };
      axios
        .put('https://localhost:7051/api/Products/UpdateProduct', payload)
        .then((response) => {
          navigate("/");
        });
    }

    return (
      <>
      <legend>Update Products</legend>
      <form>
      <Form.Group className="mb-3" controlId="formName">
          <Form.Label>Id</Form.Label>
          <Form.Control type="int" ref={id} />
        </Form.Group>
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
      <Button variant="primary" type="button" onClick={updateProductHandler}>
        Submit
      </Button>
      </>
    );

  }
  export default UpdateProduct;