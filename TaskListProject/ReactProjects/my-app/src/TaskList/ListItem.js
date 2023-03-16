import React from "react";
import "./TaskList.css";

function ListItem({ item, index, handleCompleteItem }) {
  return (
    <li className="list-item">
      <label>
        <input
          type="checkbox"
          onChange={() => handleCompleteItem(item, index)}
          checked={false}
        />
        <span></span>
      </label>
      <span className="list-item-text">{item.title}</span>
    </li>
  );
}

export default ListItem;
