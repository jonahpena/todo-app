import { render, screen } from "@testing-library/react";
import ListItem from "./ListItem";

test("renders ListItem component", () => {
  const item = {
    id: 1,
    title: "Sample task",
  };

  render(
    <ListItem
      item={item}
      index={0}
      handleRemoveItem={() => {}}
      handleCompleteItem={() => {}}
      handleUpdateItem={() => {}}
      handleEditingTask={() => {}}
      editingItemIdRef={{ current: null }}
    />
  );

  expect(screen.getByText(/Sample task/i)).toBeInTheDocument();
});

// Add more tests to cover the component's functionality
