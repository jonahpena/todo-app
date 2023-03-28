import React from "react";
import "./TaskList.css";
import ListItem from "./ListItem";

function TaskListItems({
  items,
  handleRemoveItem,
  handleCompleteItem,
  handleUpdateItem,
  handleEditingTask,
}) {
  return (
    <ul data-testid="task-list">
      {[...items].reverse().map((item, index) => (
        <ListItem
          key={index}
          item={item}
          index={index}
          handleRemoveItem={handleRemoveItem}
          handleCompleteItem={handleCompleteItem}
          handleUpdateItem={handleUpdateItem}
          handleEditingTask={handleEditingTask}
        />
      ))}
    </ul>
  );
}

export default TaskListItems;
