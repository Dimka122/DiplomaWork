//import Container from "react-bootstrap/Container";
import { Container } from "react-bootstrap";
import Navbar from "react-bootstrap/Navbar";
import Nav from "react-bootstrap/Nav";
import { Link } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "./AuthContext";
const Layout = ({children}) => {
  const { user } = useContext(AuthContext);
  return (
    <>
      <Navbar expand="lg" variant="dark" bg="success">
        <Navbar.Brand>
          <Nav.Link as={Link} to="/">
            Sushi-online
          </Nav.Link>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
        <Nav>
            {user && (
              <Nav.Link as={Link} to="/">
                Fav-Products
              </Nav.Link>
            )}
          </Nav>
          <Nav className="ms-auto">
            {!user && (
              <Nav.Link as={Link} to="/login">
                Login
              </Nav.Link>
            )}
            {user && <Nav.Link href="#">{user?.email}</Nav.Link>}
          </Nav>
          
          
          
        </Navbar.Collapse>
      </Navbar>
      <Container>{children}</Container>
    </>
  );
}

 
export default Layout;