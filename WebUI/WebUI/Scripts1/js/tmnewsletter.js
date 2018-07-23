$(document).ready(function(){initTemplate();if(popup_status&&blocking_popup!=1)
{if(!user_newsletter_status)
setTimeout('setTemplate(tmnewletter_user_message)',4000);else if(user_newsletter_status==2)
setTimeout('setTemplate(tmnewletter_guest_message)',4000);$(document).on('click','#newsletter_popup',function(event){event.stopPropagation();});$(document).on('click','.tmnewsletter-close, .newsletter-overlay',function(){closePopup();updateDate();});$(document).on('click','.tmnewsletter-submit',function(){submitNewsletter();});}});function initTemplate()
{tmnewletter_user_message='';tmnewletter_user_message+='<div id="newsletter_popup" class="tmnewsletter">';tmnewletter_user_message+='<div class="tmnewsletter-inner">';tmnewletter_user_message+='<div class="tmnewsletter-close icon">';tmnewletter_user_message+='</div>';tmnewletter_user_message+='<div class="tmnewsletter-header">';tmnewletter_user_message+='<h4>'+text_heading+'</h4>';tmnewletter_user_message+='</div>';tmnewletter_user_message+='<div class="tmnewsletter-content">';tmnewletter_user_message+='<div class="status-message"></div>';tmnewletter_user_message+='<div class="description">'+text_description+'</div>';tmnewletter_user_message+='<div class="form-group">';tmnewletter_user_message+='<input class="form-control" placeholder="'+text_placeholder+'" type="email" name="email" />';tmnewletter_user_message+='</div>';tmnewletter_user_message+='<button class="btn btn-default tmnewsletter-submit"><span>'+text_sign+'</span></button>';tmnewletter_user_message+='</div>';tmnewletter_user_message+='<div class="tmnewsletter-footer">';tmnewletter_user_message+='</div>';tmnewletter_user_message+='</div>';tmnewletter_user_message+='</div>';tmnewletter_guest_message='';tmnewletter_guest_message+='<div id="newsletter_popup" class="tmnewsletter tmnewsletter-autorized">';tmnewletter_guest_message+='<div class="tmnewsletter-inner">';tmnewletter_guest_message+='<div class="tmnewsletter-close icon">';tmnewletter_guest_message+='</div>';tmnewletter_guest_message+='<div class="tmnewsletter-header">';tmnewletter_guest_message+='<h4>'+text_heading+'</h4>';tmnewletter_guest_message+='</div>';tmnewletter_guest_message+='<div class="tmnewsletter-content">';tmnewletter_guest_message+='<div class="status-message"></div>';tmnewletter_guest_message+='<div class="description">'+text_description+'</div>';tmnewletter_guest_message+='<div class="form-group">';tmnewletter_guest_message+='<input class="form-control" placeholder="'+text_placeholder+'" type="email" name="email" />';tmnewletter_guest_message+='</div>';tmnewletter_guest_message+='<button class="btn btn-default tmnewsletter-submit"><span>'+text_sign+'</span></button>';tmnewletter_guest_message+='</div>';tmnewletter_guest_message+='<div class="tmnewsletter-footer">';tmnewletter_guest_message+='<div class="checkbox"><input type="checkbox" name="disable_popup" />'+text_remove+'</div>';tmnewletter_guest_message+='</div>';tmnewletter_guest_message+='</div>';tmnewletter_guest_message+='</div>';}
function setTemplate(html)
{$('body').append('<div class="newsletter-overlay">'+html+'</div>');$("#newsletter_popup .checkbox input").uniform();}
function displaySuccess(message)
{successMessage='';successMessage+='<div class="success-message alert alert-success">'+message+'</div>';$('body').find('#newsletter_popup .status-message').html(successMessage);}
function displayError(message)
{errormessage='';errormessage+='<div class="error-message alert alert-danger">'+message+'</div>';$('body').find('#newsletter_popup .status-message').html(errormessage);}
function closePopup()
{$('#newsletter_popup, .newsletter-overlay').fadeOut(300,function(){$('#newsletter_popup').remove()});}
function validateEmail(email)
{var emailReg=/^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;return emailReg.test(email);}
function submitNewsletter()
{$('#newsletter_popup.tmnewsletter-autorized .checker > span').hasClass('checked')?status=1:status=0;email_field=$('#newsletter_popup').find('input');email=email_field.val();if(!email||!validateEmail(email))
email_field.css('border-color','red');else
{$.ajax({type:'POST',url:baseDir+'modules/tmnewsletter/tmnewsletter-ajax.php',data:'action=sendemail&email='+email+'&status='+status+'&is_logged='+is_logged,dataType:"json",success:function(response){if(response.success_status)
{displaySuccess(response.success_status);}
else
{displayError(response.error_status);}}});}}
function updateDate()
{$('#newsletter_popup.tmnewsletter-autorized .checker > span').hasClass('checked')?status=1:status=0;$.ajax({type:'POST',url:baseDir+'modules/tmnewsletter/tmnewsletter-ajax.php',async:true,cache:false,dataType:"json",data:{action:'updatedate',status:status,},error:function(XMLHttpRequest,textStatus,errorThrown)
{error="TECHNICAL ERROR: unable to close form.\n\nDetails:\nError thrown: "+ XMLHttpRequest+"\n"+'Text status: '+ textStatus;alert(error);}});}