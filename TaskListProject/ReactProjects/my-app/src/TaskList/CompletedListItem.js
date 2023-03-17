import React from "react";
import "./TaskList.css";

function CompletedListItem({
  item,
  completedItems,
  handleMoveBackToTask,
  handleRemoveItem,
}) {
  const handleCheckboxChange = (e) => {
    console.log("checkbox changed");
    console.log("item:", item);
    console.log("completedItems:", completedItems);
    if (!e.target.checked) {
      handleMoveBackToTask(item);
    }
  };

  return (
    <li className="completed-list-bubble">
      <div className="completed-list-item">
        <label className="filled-in">
          <input
            type="checkbox"
            className="complete-button"
            checked
            style={{ color: "white" }}
            onChange={handleCheckboxChange}
          />
          <span></span>
        </label>
        <div className="completed-list-item-text">{item.title}</div>
        <span
          className="material-icons delete-icon"
          onClick={() => handleRemoveItem(item.id)}
        >
          close
        </span>
      </div>
      {/* <span className="completed-time">{item.time}</span> */}
    </li>
  );
}

export default CompletedListItem;
