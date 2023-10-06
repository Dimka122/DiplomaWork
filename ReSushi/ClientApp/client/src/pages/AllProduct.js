import { Row } from "react-bootstrap";
import Card from "react-bootstrap/Card";
import Col from "react-bootstrap/Col";
import { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import Button from "react-bootstrap/Button";
import DeleteConfirmation from "../components/shared/DeleteConfirmation";
 
function AllProduct() {
  const [products, setProducts] = useState([]);
  const navigate = useNavigate ();

  const [showModal, setShowModal] = useState(false);
  const [itemToDeleteId, setItemToDeleteId] = useState(0);
 
  useEffect(() => {
    axios.get("https://localhost:7051/api/Products/GetProducts").then((response) => {
      setProducts((data) => {
        return response.data;
      });
    });
  }, []);

  function confirmDeleteHandler() {
    axios
      .delete(`https://localhost:7051/Products/${itemToDeleteId}`)
      .then((response) => {
        setShowModal(false);
        setProducts((existingData) => {
          return existingData.filter((_) => _.id !== itemToDeleteId);
        });
        setItemToDeleteId(0);
      });
  }
 
  function showConfirmDeleteHandler(id) {
    setShowModal(true);
    setItemToDeleteId(id);
  }
 
  function hideConfirmDeleteHandler() {
    setShowModal(false);
 
    setItemToDeleteId(0);
  }
 
  return (
    
        <>

<DeleteConfirmation
        showModal={showModal}
        title="Delete Confirmation"
        body="Are you want delete this itme?"
        confirmDeleteHandler={confirmDeleteHandler}
        hideConfirmDeleteHandler={hideConfirmDeleteHandler}
      ></DeleteConfirmation>

          <Row className="mt-2">
            <Col md={{ span: 4, offset: 4 }}>
              <Button
                variant="primary"
                type="button"
                onClick={() => navigate("/AddProduct")}
              >
                Add Products
              </Button>
            </Col>
          </Row>

          <Row md={3} className="g-4 mt-1">
            {products.map((sv) => {
              return (
            <Col key={sv.id}>
              <Card>
                <Card.Img variant="top" src={sv.imageUrl} />
                <Card.Body>
                  <Card.Title>{sv.name}</Card.Title>
                  <Card.Text>
                    <b>Category:</b> {sv.category}
                  </Card.Text>
                  <Card.Text>
                    <b>Detail: </b>
                    {sv.detail}
                  </Card.Text>
                  
                    {sv.DOJ}
                  
                  <Button
                    variant="primary"
                    onClick={() => navigate(`/UpdateProduct`)}
                  >
                    Edit
                  </Button>
                  <Button
                    type="button"
                    variant="danger"
                    onClick={() => showConfirmDeleteHandler(sv.id)}
                  >
                    Delete
                  </Button>
                </Card.Body>
                
              </Card>

            </Col>
            
          );
        })}
      </Row>
    </>
  );
}
 
export default AllProduct;