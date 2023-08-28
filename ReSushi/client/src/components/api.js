import axios from 'axios';

const baseURL = 'http://localhost:7051/api';

export const getAllProducts = async () => {
  try {
    const response = await axios.get(`${baseURL}/Products/GetAll`);
    return response.data;
  } catch (error) {
    console.error('Error fetching products:', error);
    return [];
  }
};

export const getCategories = async () => {
  try {
    const response = await axios.get(`${baseURL}/Categories/GetAll`);
    return response.data;
  } catch (error) {
    console.error('Error fetching categories:', error);
    return [];
  }
};

export const addOrUpdateProduct = async (product) => {
    try {
      if (product.id === 0) {
        const response = await axios.post(`${baseURL}/Products/AddOrUpdate`, { product });
        console.log('Add product response:', response); // Добавим эту строку для отладки
        return response.data;
      } else {
        const response = await axios.put(`${baseURL}/Products/AddOrUpdate`, { product });
        console.log('Update product response:', response); // Добавим эту строку для отладки
        return response.data;
      }
    } catch (error) {
      console.error('Error adding/updating product:', error);
      console.error('Error details:', error.response ? error.response.data : 'No response data available'); // Добавим эту строку для вывода дополнительной информации
      return null;
    }
  };
  
  

export const deleteProduct = async (productId) => {
  try {
    await axios.delete(`${baseURL}/Products/Delete/${productId}`);
  } catch (error) {
    console.error('Error deleting product:', error);
  }
};
