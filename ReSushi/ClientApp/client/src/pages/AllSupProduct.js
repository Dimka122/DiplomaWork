import { Row } from "react-bootstrap";
import Card from "react-bootstrap/Card";
import Col from "react-bootstrap/Col";
import { useEffect, useState } from "react";
import axios from "axios";


function AllSupProduct() {
    const [product, setProduct] = useState([]);
 
    useEffect(() => {
      axios.get("https://localhost:7051/api/Products").then((response) => {
        setProduct((data) => {
          return response.data;
        });
      });
    }, []);

    return <>
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
                </Card.Body>
              </Card>
            </Col>
          );
        })}
      </Row>
    </>;
  }
  export default AllSupProduct;