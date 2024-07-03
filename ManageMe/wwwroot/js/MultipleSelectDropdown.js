function MultiselectDropdown(containerId, formValuesContainerId, formFieldName, items, selectedValues = []) {
    let container = document.getElementById(containerId);
    let formValuesContainer = document.getElementById(formValuesContainerId);
    this.selectedValues = selectedValues;
    this.items = items;

    let select = document.createElement("select");
    select.className = "form-control";
    let placeholderOption = document.createElement("option");
    placeholderOption.disabled = true;
    placeholderOption.selected = true;
    placeholderOption.value = "";
    placeholderOption.text = "Select..."
    select.appendChild(placeholderOption);

    this.items.forEach(item => {
        let option = document.createElement("option");
        option.value = item.value;
        option.selected = item.selected;
        option.text = item.text;
        select.appendChild(option);
    });

    select.onchange = e => {
        this.items.map(item => item.selected = item.value == e.currentTarget.value ? true : item.selected);
        this.selectedValues = this.items.filter(item => item.selected).map(i => i.value);
        this.updateSelectedOptionsDiv();
    }

    this.updateSelectedOptionsDiv = () => {
        selectedOptionsDiv.innerHTML = null;
        this.items.filter(item => item.selected).forEach(item => {
            let selectedOptionSpan = document.createElement("span");
            selectedOptionSpan.textContent = item.text;
            selectedOptionSpan.onclick = e => this.onSelectedOptionClick(e, item.value);
            selectedOptionsDiv.appendChild(selectedOptionSpan);
        });
        this.updateFormValues();
    }
    this.onSelectedOptionClick = (e, value) => {
        this.items.map(item => item.selected = item.value == value ? false : item.selected);
        this.selectedValues = this.items.filter(item => item.Selected).map(i => i.value);
        this.updateSelectedOptionsDiv();
    }
    this.updateFormValues = () => {
        formValuesContainer.innerHTML = null;
        this.selectedValues.forEach(value => {
            let valueInput = document.createElement("input");
            valueInput.type = "hidden";
            valueInput.name = formFieldName;
            valueInput.value = value;
            formValuesContainer.appendChild(valueInput);
        });
    }

    container.appendChild(select);

    let selectedOptionsDiv = document.createElement("div");
    selectedOptionsDiv.className = "form-multiple-options";
    container.appendChild(selectedOptionsDiv);

    this.updateSelectedOptionsDiv();
}