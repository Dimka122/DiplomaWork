import { Row } from "react-bootstrap";
import Card from "react-bootstrap/Card";
import Col from "react-bootstrap/Col";
import { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import Button from "react-bootstrap/Button";


function AllSupProduct() {
    const [product, setProduct] = useState([]);
    const navigate = useNavigate();
 
    useEffect(() => {
      axios.get("https:/localhost:7051/api/Products").then((response) => {
        setProduct((data) => {
          return response.data;
        });
      });
    }, []);

    
    return (
      <>
        <Row className="mt-2">
          <Col md={{ span: 4, offset: 4 }}>
            <Button
              variant="primary"
              type="button"
              onClick={() => navigate("/product-create")}
            >
              Add A Products
            </Button>
          </Col>
        </Row>
        
              
    <Row md={3} className="g-4 mt-1">
        {product.map((sv) => {
          return (
            <Col key={sv.id}>
              <Card>
                <Card.Img variant="top" src={sv.imageUrl} />
                <Card.Body>
                  <Card.Title>{sv.Name}</Card.Title>
                  <Card.Text>
                    <b>Price</b> {sv.price}
                  </Card.Text>
                  <Card.Text>
                    <b>Detail </b>
                    {sv.detail}
                  </Card.Text>
                  <Button
                    variant="primary"
                    onClick={() => navigate(`/product-update/${sv.id}`)}
                  >
                    Edit
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
  export default AllSupProduct;