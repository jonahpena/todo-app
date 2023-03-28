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
      <div>
        <div className="completed-list-item">
          <label className="filled-in">
            <input
              data-testid="task-checkbox"
              data-index={0} // Add this line
              type="checkbox"
              className="complete-button"
              checked
              style={{ color: "white" }}
              onChange={handleCheckboxChange}
            />
            <span></span>
          </label>
          <div className="completed-list-item-text">{item.title}</div>
        </div>{" "}
        <div className="completed-delete-icon">
          {" "}
          <span
            className="material-icons"
            onClick={() => handleRemoveItem(item.id)}
          >
            close
          </span>
        </div>
        {/* <span className="completed-time">{item.time}</span> */}
      </div>
    </li>
  );
}

export default CompletedListItem;
