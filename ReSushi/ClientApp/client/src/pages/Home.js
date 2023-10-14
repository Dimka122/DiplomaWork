import Card from "react-bootstrap/Card";
const Home = () => {
  return (
    <>
      <div
        className="d-flex justify-content-center align-items-center"
        style={{ minHeight: "500px", minWidth: "600px" }}
      >
        <Card>
          <Card.Body>
            <Card.Text className="text-center">
              <b>Добро пожаловать в наш онлайн-магазин "Sushi"!</b>
              <b>Для просмотра товара зарегистрируйтесь, пожалуйста!</b>
            </Card.Text>
          </Card.Body>
        </Card>
      </div>
    </>
  );
};
export default Home;