import React, { useState } from 'react';

function ToDoList() {
  const [items, setItems] = useState([]);
  const [inputValue, setInputValue] = useState('');

  const handleAddItem = () => {
    setItems([...items, inputValue]);
    setInputValue('');
  };

  const handleRemoveItem = (index) => {
    setItems(items.filter((item, i) => i !== index));
  };

  return (
    <div>
      <h1>To-Do List</h1>
      <input
        type="text"
        value={inputValue}
        onChange={(e) => setInputValue(e.target.value)}
      />
      <button onClick={handleAddItem}>Add</button>
      <ul>
        {items.map((item, index) => (
          <li key={index}>
            {item}
            <button onClick={() => handleRemoveItem(index)}>Remove</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ToDoList;
