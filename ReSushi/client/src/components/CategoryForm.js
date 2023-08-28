import React, { useState } from 'react';
import axios from 'axios';

const CategoryForm = ({ onCategoryAdded }) => {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    const newCategory = { name, description };

    try {
      const response = await axios.post('/api/Categories/Add', newCategory);
      onCategoryAdded(response.data); // Обновляем список категорий после добавления
      setName('');
      setDescription('');
    } catch (error) {
      console.error('Error adding category:', error);
    }
  };

  return (
    <div>
      <h3>Add New Category</h3>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Name:</label>
          <input type="text" value={name} onChange={(e) => setName(e.target.value)} />
        </div>
        <div>
          <label>Description:</label>
          <input type="text" value={description} onChange={(e) => setDescription(e.target.value)} />
        </div>
        <button type="submit">Add Category</button>
      </form>
    </div>
  );
};

export default CategoryForm;
