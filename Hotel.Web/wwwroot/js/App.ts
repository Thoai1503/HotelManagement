import $ from "jquery";
import AppNavigator from "./Controllers/AppNavigator";

const navigator = new AppNavigator();

const initializePages = (): void => {
  console.log(
    "Initializing pages...:" +
      JSON.stringify($("[page-controller][page-action]")),
  );
  $("[page-controller][page-action]").each((_, page) =>
    console.log(
      `Initializing page: ${$(page).attr("page-controller")}/${$(page).attr("page-action")}`,
    ),
  );
  $("[page-controller][page-action]").each((_, page) =>
    navigator.init($(page)),
  );
};

$(initializePages);
